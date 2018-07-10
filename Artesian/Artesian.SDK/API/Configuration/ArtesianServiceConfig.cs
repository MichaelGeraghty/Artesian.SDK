using System;
using System.Collections.Generic;
using System.Text;

namespace Artesian.SDK.API.Configuration
{
    public class ArtesianServiceConfig
    {
        public Uri BaseAddress { get; set; }
        public string Audience { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
