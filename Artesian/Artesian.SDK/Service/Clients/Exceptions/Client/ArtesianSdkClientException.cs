﻿using System;

namespace Artesian.SDK.Service
{
    public class ArtesianSdkClientException : Exception
    {
        public ArtesianSdkClientException(string message)
            : base(message)
        {
        }

        public ArtesianSdkClientException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }

        public ArtesianSdkClientException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}