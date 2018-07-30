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
        public string XApiKey { get; set; }

        public ArtesianServiceConfig() { }

        public ArtesianServiceConfig(Uri baseAddress, string xApiKey)
        {
            BaseAddress = baseAddress;
            XApiKey = xApiKey;
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
