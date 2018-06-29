using Artesian.SDK.API.Exceptions.Client;

namespace Artesian.SDK.API.MarketData.Exception
{
    public class MarketAssessmentException : ArtesianSdkClientException
    {
        public MarketAssessmentException(string message)
            : base(message)
        {
        }

        public MarketAssessmentException(string format, params object[] args)
            : base(format, args)
        {
        }
    }
}
