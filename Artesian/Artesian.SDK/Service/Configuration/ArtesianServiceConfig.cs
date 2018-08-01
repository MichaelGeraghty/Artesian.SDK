using System;

namespace Artesian.SDK.Service
{
    public class ArtesianServiceConfig: IArtesianServiceConfig
    {
        public Uri BaseAddress { get; set; }
        public string Audience { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiKey { get; set; }

        public ArtesianServiceConfig() { }

        public ArtesianServiceConfig(Uri baseAddress, string xApiKey)
        {
            BaseAddress = baseAddress;
            ApiKey = xApiKey;
        }

        public ArtesianServiceConfig(Uri baseAddress, string audience, string domain, string clientId, string clientSecret)
        {
            BaseAddress = baseAddress;
            Audience = audience;
            Domain = domain;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}
