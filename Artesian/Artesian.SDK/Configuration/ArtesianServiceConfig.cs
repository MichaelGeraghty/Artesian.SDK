using Artesian.SDK.Configuration.Interface;
using System;

namespace Artesian.SDK.Configuration
{
    public class ArtesianServiceConfig: IArtesianServiceConfig
    {
        public Uri BaseAddress { get; set; }
        public string Audience { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
