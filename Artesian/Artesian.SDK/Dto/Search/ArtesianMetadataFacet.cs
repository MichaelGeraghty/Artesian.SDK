// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack;
using System.Collections.Generic;

namespace Artesian.SDK.Dto
{
    [MessagePackObject]
    public class ArtesianMetadataFacet
    {
        [Key(0)]
        public string FacetName { get; set; }
        [Key(1)]
        public ArtesianMetadataFacetType FacetType { get; set; }
        [Key(2)]
        public List<ArtesianMetadataFacetCount> Values { get; set; }
    }

    [MessagePackObject]
    public class ArtesianMetadataFacetCount
    {
        [Key(0)]
        public string Value { get; set; }
        [Key(1)]
        public long? Count { get; set; }
    }

    public enum ArtesianMetadataFacetType
    {
        Property = 0
        , Tag
    }
}
