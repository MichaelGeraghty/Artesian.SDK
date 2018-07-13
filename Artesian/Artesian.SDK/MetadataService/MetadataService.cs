using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Artesian.SDK.Clients;
using Artesian.SDK.Configuration;
using Artesian.SDK.Dependencies.Common;
using Artesian.SDK.Dto.PagedResult;
using Artesian.SDK.Dto.Search;
using Artesian.SDK.Dto.TimeTransform.Dto;
using Artesian.SDK.MetadataService.Interface;
using Flurl;

namespace Artesian.SDK.MetadataService
{
    public class MetadataService : IMetaDataService
    {
        private IArtesianServiceConfig _cfg;
        private static Auth0Client _client;

        public MetadataService(IArtesianServiceConfig cfg)
        {
            throw new Exception("Provide url");
            _cfg = cfg;
            _client = new Auth0Client(cfg, "" //fix this!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            );
        }

        public Task<TimeTransformBase> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default)
        {
            return _client.Exec<TimeTransformBase>(HttpMethod.Get, $@"/timeTransform/entity/{timeTransformId}", ctk: ctk);
        }

        public Task<PagedResult<TimeTransformBase>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default)
        {
            var url = "/timeTransform/entity"
                    .SetQueryParam("pageSize", pageSize)
                    .SetQueryParam("page", page)
                    .SetQueryParam("userDefined", userDefined)
                    ;

            return _client.Exec<PagedResult<TimeTransformBase>>(HttpMethod.Get, url.ToString(), ctk: ctk);
        }

        public Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default)
        {
            var url = "/marketdata/searchfacet"
                    .SetQueryParam("pageSize", filter.PageSize)
                    .SetQueryParam("page", filter.Page)
                    .SetQueryParam("searchText", filter.SearchText)
                    .SetQueryParam("filters", filter.Filters?.SelectMany(s => s.Value.Select(x => $@"{s.Key}:{x}")))
                    .SetQueryParam("sorts", filter.Sorts)
                    ;

            return _client.Exec<ArtesianSearchResults>(HttpMethod.Get, url.ToString());
        }
    }
}
