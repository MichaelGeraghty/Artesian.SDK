using Artesian.SDK.API.Exceptions.Client;

namespace Artesian.SDK.API.MarketData.Exception
{
    public class ActualTimeSerieException : ArtesianSdkClientException
    {
        public ActualTimeSerieException(string message)
            : base(message)
        {
        }

        public ActualTimeSerieException(string format, params object[] args)
            : base(format, args)
        {
        }
    }
}
