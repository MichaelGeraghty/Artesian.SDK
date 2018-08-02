// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack;
using System.Collections.Generic;

namespace Artesian.SDK.Dto
{
    [MessagePackObject]
    public class ArtesianSearchResults
    {
        [Key(0)]
        public List<MarketDataEntity.Output> Results { get; set; }
        [Key(1)]
        public List<ArtesianMetadataFacet> Facets { get; set; }
        [Key(2)]
        public long? CountResults { get; set; }
    }
}
