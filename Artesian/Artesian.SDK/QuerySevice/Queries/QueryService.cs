using Artesian.SDK.Clients;
using Artesian.SDK.Configuration;
using Artesian.SDK.Dependencies.Common.DTO;
using Artesian.SDK.QueryService.Interface;
using System;
using System.Net.Http;

namespace Artesian.SDK.QueryService.Queries
{
    public class QueryService: IQueryService
    {
        private ArtesianServiceConfig _cfg;
        private Auth0Client _client;

        public QueryService(ArtesianServiceConfig cfg)
        {
            throw new Exception("Provide url");
            _cfg = cfg;
            _client = new Auth0Client(cfg, () => new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            }, "" //fix this!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            );
        }

        public ActualQuery CreateActual(int[] ids, Granularity granularity)
        {
            return new ActualQuery(ids, granularity, _client);
        }

        public VersionedQuery CreateVersioned(int[] ids, Granularity granularity)
        {
            return new VersionedQuery(ids, granularity, _client);
        }

        public MasQuery CreateMarketAssessment(int[] ids)
        {
            return new MasQuery(ids, _client);
        }


    }
}
