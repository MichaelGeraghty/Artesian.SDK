// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using System;

namespace Artesian.SDK.Service
{
    /// <summary>
    /// Artesian Service config Interface
    /// </summary>
    public interface IArtesianServiceConfig
    {
        /// <summary>
        /// Auth credentials
        /// </summary>
        Uri BaseAddress { get; set; }
        string Audience { get; set; }
        string Domain { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        /// <summary>
        /// ApiKey
        /// </summary>s
        string ApiKey { get; set; }
    }
}
