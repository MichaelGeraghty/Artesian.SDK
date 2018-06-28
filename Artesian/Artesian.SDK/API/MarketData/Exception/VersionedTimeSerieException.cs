using Artesian.SDK.API.Exceptions.Client;

namespace Artesian.SDK.API.MarketData.Exception
{
    public class VersionedTimeSerieException : ArtesianSdkClientException
    {
        public VersionedTimeSerieException(string message)
            : base(message)
        {
        }

        public VersionedTimeSerieException(string format, params object[] args)
            : base(format, args)
        {
        }
    }
}
