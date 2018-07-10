using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Artesian.SDK.Clients;
using Artesian.SDK.Common.Dto.Search;
using Artesian.SDK.Configuration.Interface;
using Artesian.SDK.Dependencies.Common;
using Artesian.SDK.Dependencies.Common.Dto.PagedResult;
using Artesian.SDK.Dependencies.Common.Dto.Search;
using Artesian.SDK.Dependencies.Common.Dto.TimeTransform.Dto;
using Artesian.SDK.MetadataService.Interface;

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
            _client = new Auth0Client(cfg, () => new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            }, "" //fix this!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            );
        }

        public Task<TimeTransformBase> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default)
        {
            return _client.Exec<TimeTransformBase>(HttpMethod.Get, $@"/timeTransform/entity/{timeTransformId}", ctk: ctk);
        }

        public Task<PagedResult<TimeTransformBase>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default)
        {
            var url = new UrlComposer("/timeTransform/entity")
                    .AddQueryParam("pageSize", $"{pageSize}")
                    .AddQueryParam("page", $"{page}")
                    .AddQueryParam("userDefined", userDefined)
                    ;

            return _client.Exec<PagedResult<TimeTransformBase>>(HttpMethod.Get, url, ctk: ctk);
        }

        public Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default)
        {
            var url = new UrlComposer("/marketdata/searchfacet")
                    .AddQueryParam("pageSize", $"{filter.PageSize}")
                    .AddQueryParam("page", $"{filter.Page}")
                    .AddQueryParam("searchText", filter.SearchText)
                    .AddQueryParam("filters", filter.Filters?.SelectMany(s => s.Value.Select(x => $@"{s.Key}:{x}")))
                    .AddQueryParam("sorts", filter.Sorts)
                    ;

            return _client.Exec<ArtesianSearchResults>(HttpMethod.Get, url);
        }
    }
}
