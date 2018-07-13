using Artesian.SDK.Clients;
using Artesian.SDK.Configuration;
using Artesian.SDK.Dto;
using Artesian.SDK.QueryService.Interface;
using System;

namespace Artesian.SDK.QueryService.Queries
{
    public class QueryService: IQueryService
    {
        private IArtesianServiceConfig _cfg;
        private Auth0Client _client;

        public QueryService(IArtesianServiceConfig cfg)
        {
            //throw new Exception("Provide url");
            _cfg = cfg;
            _client = new Auth0Client(cfg, "" //fix this!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            );
        }

        public ActualQuery CreateActual()
        {
            return new ActualQuery(_client);
        }

        public VersionedQuery CreateVersioned()
        {
            return new VersionedQuery(_client);
        }

        public MasQuery CreateMarketAssessment()
        {
            return new MasQuery(_client);
        }


    }
}
