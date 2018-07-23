using MessagePack;
using System.Collections.Generic;

namespace Artesian.SDK.Dto
{
    [MessagePackObject]
    public class ArtesianMetaDataFacet
    {
        [Key(0)]
        public string FacetName { get; set; }
        [Key(1)]
        public ArtesianMetaDataFacetType FacetType { get; set; }
        [Key(2)]
        public List<ArtesianMetaDataFacetCount> Values { get; set; }
    }

    [MessagePackObject]
    public class ArtesianMetaDataFacetCount
    {
        [Key(0)]
        public string Value { get; set; }
        [Key(1)]
        public long? Count { get; set; }
    }

    public enum ArtesianMetaDataFacetType
    {
        Property = 0
        , Tag
    }
}
