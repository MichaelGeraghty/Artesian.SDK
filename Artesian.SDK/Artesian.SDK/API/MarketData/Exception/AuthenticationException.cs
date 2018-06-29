using Artesian.SDK.API.Exceptions.Client;

namespace Artesian.SDK.API.MarketData.Exception
{
    public class AuthenticationException : ArtesianSdkClientException
    {
        public AuthenticationException(string message)
            : base(message)
        {
        }

        public AuthenticationException(string message, System.Exception innerEx)
            : base(message, innerEx)
        {
        }

        public AuthenticationException(string format, params object[] args)
            : base(format, args)
        {
        }
    }
}
