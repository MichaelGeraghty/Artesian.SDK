using System;

namespace Artesian.SDK
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
