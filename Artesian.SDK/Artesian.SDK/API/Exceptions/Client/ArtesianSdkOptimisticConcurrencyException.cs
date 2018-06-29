using Artesian.SDK.API.Exceptions.Remote;
using System;

namespace Artesian.SDK.API.Exceptions.Client
{
    public class ArtesianSdkOptimisticConcurrencyException : ArtesianSdkRemoteException
    {
        public ArtesianSdkOptimisticConcurrencyException(string message)
            : base(message)
        {
        }

        public ArtesianSdkOptimisticConcurrencyException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }

        public ArtesianSdkOptimisticConcurrencyException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}
