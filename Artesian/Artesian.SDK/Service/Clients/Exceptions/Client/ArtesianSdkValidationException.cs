// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using System;

namespace Artesian.SDK.Service
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
