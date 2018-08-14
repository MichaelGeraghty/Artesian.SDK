// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using System;

namespace Artesian.SDK.Service
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
