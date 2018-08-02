// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using System;

namespace Artesian.SDK.Service
{
    public interface IArtesianServiceConfig
    {
        Uri BaseAddress { get; set; }
        string Audience { get; set; }
        string Domain { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string ApiKey { get; set; }
    }
}
