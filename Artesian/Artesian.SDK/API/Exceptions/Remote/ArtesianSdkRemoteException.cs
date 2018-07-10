using System;

namespace Artesian.SDK.API.Exceptions.Remote
{
    public class ArtesianSdkRemoteException : Exception
    {
        public ArtesianSdkRemoteException(string message)
            : base(message)
        {
        }

        public ArtesianSdkRemoteException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }

        public ArtesianSdkRemoteException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}
