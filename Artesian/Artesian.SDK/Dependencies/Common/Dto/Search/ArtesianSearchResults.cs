using Artesian.SDK.Dependencies.Common.Dto.Api.V2;
using Artesian.SDK.Dependencies.Common.Dto.Search;
using MessagePack;
using System.Collections.Generic;

namespace Artesian.SDK.Common.Dto.Search
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
