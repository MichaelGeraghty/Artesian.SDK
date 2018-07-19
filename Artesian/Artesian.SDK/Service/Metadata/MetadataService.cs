using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Artesian.SDK.Dto;
using Flurl;

namespace Artesian.SDK.Service
{
    public class MetadataService : IMetaDataService
    {
        private IArtesianServiceConfig _cfg;
        private static Auth0Client _client;

        public MetadataService(IArtesianServiceConfig cfg)
        {
            _cfg = cfg;
            _client = new Auth0Client(cfg, cfg.BaseAddress.ToString().AppendPathSegment(ArtesianConstants.MetadataVersion)
            );
        }

        public Task<TimeTransform> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default)
        {
            return _client.Exec<TimeTransform>(HttpMethod.Get, $@"/timeTransform/entity/{timeTransformId}", ctk: ctk);
        }

        public Task<PagedResult<TimeTransform>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default)
        {
            var url = "/timeTransform/entity"
                    .SetQueryParam("pageSize", pageSize)
                    .SetQueryParam("page", page)
                    .SetQueryParam("userDefined", userDefined)
                    ;

            return _client.Exec<PagedResult<TimeTransform>>(HttpMethod.Get, url.ToString(), ctk: ctk);
        }

        public Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default)
        {
            filter.Validate();

            var url = "/marketdata/searchfacet"
                    .SetQueryParam("pageSize", filter.PageSize)
                    .SetQueryParam("page", filter.Page)
                    .SetQueryParam("searchText", filter.SearchText)
                    .SetQueryParam("filters", filter.Filters?.SelectMany(s => s.Value.Select(x => $@"{s.Key}:{x}")))
                    .SetQueryParam("sorts", filter.Sorts)
                    ;

            return _client.Exec<ArtesianSearchResults>(HttpMethod.Get, url.ToString(), ctk: ctk);
        }

        public Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(MarketDataIdentifier id, CancellationToken ctk = default)
        {
            var url = "/marketdata/entity"
                    .SetQueryParam("provider", id.Provider)
                    .SetQueryParam("name", id.Name)
                    ;
            return _client.Exec<MarketDataEntity.V2.Output>(HttpMethod.Get, url.ToString(), ctk: ctk);
        }

        public Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(int id, CancellationToken ctk = default)
        {
            var url = "/marketdata/entity/{id}"
                    .SetQueryParam("id",id)
                    ;
            return _client.Exec<MarketDataEntity.V2.Output>(HttpMethod.Get, url.ToString(), ctk: ctk);
        }
    }
}
