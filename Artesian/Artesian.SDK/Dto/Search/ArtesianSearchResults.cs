using Artesian.SDK.Dto.Api.V2;
using MessagePack;
using System.Collections.Generic;

namespace Artesian.SDK.Dto.Search
{
    [MessagePackObject]
    public class ArtesianSearchResults
    {
        [Key(0)]
        public List<MarketDataEntity.V2.Output> Results { get; set; }
        [Key(1)]
        public List<ArtesianMetadataFacet> Facets { get; set; }
        [Key(2)]
        public long? CountResults { get; set; }
    }
}
