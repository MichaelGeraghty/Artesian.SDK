using Artesian.SDK.API.Exceptions.Remote;
using System;

namespace Artesian.SDK.API.Exceptions.Client
{
    public class ArtesianSdkValidationException : ArtesianSdkRemoteException
    {
        public ArtesianSdkValidationException(string message)
            : base(message)
        {
        }

        public ArtesianSdkValidationException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }

        public ArtesianSdkValidationException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}
