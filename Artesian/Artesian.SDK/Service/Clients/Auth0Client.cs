﻿using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Flurl.Http;
using JWT.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Polly;
using Polly.Caching;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.Service
{
    internal sealed class Auth0Client : IDisposable
    {
        private readonly MediaTypeFormatterCollection _formatters;

        private readonly AuthenticationApiClient _auth0;
        private readonly ClientCredentialsTokenRequest _credentials;
        private readonly IFlurlClient _client;


        private readonly JsonMediaTypeFormatter _jsonFormatter;
        private readonly MessagePackFormatter _msgPackFormatter;
        private readonly LZ4MessagePackFormatter _lz4msgPackFormatter;

        private readonly object _gate = new { };

        private readonly string _url;

        private readonly Polly.Caching.Memory.MemoryCacheProvider _memoryCacheProvider
           = new Polly.Caching.Memory.MemoryCacheProvider(new Microsoft.Extensions.Caching.Memory.MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()));

        private readonly Policy<(string AccessToken, DateTimeOffset ExpiresOn)> _cachePolicy;

        public IArtesianServiceConfig Config { get; private set; }

        public Auth0Client(IArtesianServiceConfig config, string Url)
        {

            this.Config = config;
            this._url = Url;

            var cfg = new JsonSerializerSettings();
            cfg = cfg.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            cfg = cfg.ConfigureForDictionary();
            cfg = cfg.ConfigureForNodaTimeRanges();
            cfg.Formatting = Formatting.Indented;
            cfg.ContractResolver = new DefaultContractResolver();
            cfg.Converters.Add(new StringEnumConverter());
            cfg.TypeNameHandling = TypeNameHandling.None;
            cfg.ObjectCreationHandling = ObjectCreationHandling.Replace;

            var jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.SerializerSettings = cfg;
            _jsonFormatter = jsonFormatter;

            _msgPackFormatter = new MessagePackFormatter(CustomCompositeResolver.Instance);
            _lz4msgPackFormatter = new LZ4MessagePackFormatter(CustomCompositeResolver.Instance);

            var formatters = new MediaTypeFormatterCollection();
            formatters.Clear();
            formatters.Add(_jsonFormatter);
            formatters.Add(_msgPackFormatter);
            formatters.Add(_lz4msgPackFormatter);
            _formatters = formatters;

            // configure webclient
            _auth0 = new AuthenticationApiClient($"{Config.Domain}");
            _credentials = new ClientCredentialsTokenRequest()
            {
                Audience = Config.Audience,
                ClientId = Config.ClientId,
                ClientSecret = Config.ClientSecret,
            };


            _client = new FlurlClient(_url);
            _client.Settings.HttpClientFactory.CreateHttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            });

            _client.HttpClient.BaseAddress = this.Config.BaseAddress;
            _client.HttpClient.DefaultRequestHeaders.Accept.Clear();
            _client.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(LZ4MessagePackFormatter.DefaultMediaType.MediaType, 1));
            _client.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MessagePackFormatter.DefaultMediaType.MediaType, 0.9));
            _client.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaTypeFormatter.DefaultMediaType.MediaType, 0.8));

            _cachePolicy = Policy.CacheAsync(_memoryCacheProvider.AsyncFor<(string AccessToken, DateTimeOffset ExpiresOn)>(), new ResultTtl<(string AccessToken, DateTimeOffset ExpiresOn)>(r => new Ttl(r.ExpiresOn - DateTimeOffset.Now, false)));

        }


        public async Task<TResult> Exec<TResult, TBody>(HttpMethod method, string resource, TBody body = default(TBody), CancellationToken ctk = default(CancellationToken))
        {
            try
            {
                var (token, _) = await _getAccessToken();

                using (var res = await _client.Request(resource).WithOAuthBearerToken(token).SendAsync(method))
                {
                    if (res.StatusCode == HttpStatusCode.NoContent || res.StatusCode == HttpStatusCode.NotFound)
                        return default;

                    if (!res.IsSuccessStatusCode)
                    {
                        var responseText = await res.Content.ReadAsStringAsync();

                        if (res.StatusCode == HttpStatusCode.BadRequest)
                        {
                            throw new ArtesianSdkValidationException($@"{responseText}");
                        }

                        if (res.StatusCode == HttpStatusCode.Conflict)
                            throw new ArtesianSdkOptimisticConcurrencyException($@"{responseText}");

                        throw new ArtesianSdkRemoteException("Failed handling REST call to WebInterface {0} {1}. Returned status: {2}. Content: \n{3}", method, _client.HttpClient.BaseAddress + _url + resource, res.StatusCode, responseText);
                    }

                    return await res.Content.ReadAsAsync<TResult>(_formatters, ctk);
                }
            }
            catch (ArtesianSdkRemoteException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ArtesianSdkClientException($"Failed handling REST call to WebInterface: {method} " + _client.HttpClient.BaseAddress + _url + resource, e);
            }
        }

        public async Task Exec(HttpMethod method, string resource, CancellationToken ctk = default(CancellationToken))
            => await Exec<object, object>(method, resource, null, ctk);

        public Task<TResult> Exec<TResult>(HttpMethod method, string resource, CancellationToken ctk = default(CancellationToken))
            => Exec<TResult, object>(method, resource, null, ctk);

        public async Task Exec<TBody>(HttpMethod method, string resource, TBody body, CancellationToken ctk = default(CancellationToken))
            => await Exec<object, TBody>(method, resource, body, ctk);

        #region private methods
        private async Task<(string AccessToken, DateTimeOffset ExpiresOn)> _getAccessToken()
        {
            var res = await _cachePolicy.ExecuteAsync(async (ctx) =>
            {
                var result = await _auth0.GetTokenAsync(_credentials);
                var decode = new JwtBuilder()
                    .DoNotVerifySignature()
                    .Decode<IDictionary<string, object>>(result.AccessToken);

                var exp = (long)decode["exp"];

                return (result.AccessToken, DateTimeOffset.FromUnixTimeSeconds(exp) - TimeSpan.FromMinutes(2));

            }, new Context("_getAccessToken"));

            return res;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion private methods
    }
}