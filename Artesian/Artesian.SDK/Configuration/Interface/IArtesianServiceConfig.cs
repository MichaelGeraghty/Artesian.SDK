using System;
using System.Collections.Generic;
using System.Text;

namespace Artesian.SDK.Configuration.Interface
{
    public interface IArtesianServiceConfig
    {
        Uri BaseAddress { get; set; }
        string Audience { get; set; }
        string Domain { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
    }
}
