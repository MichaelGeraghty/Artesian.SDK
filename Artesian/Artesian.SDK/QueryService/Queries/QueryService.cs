using Artesian.SDK.Clients;
using Artesian.SDK.Configuration;
using Artesian.SDK.QueryService.Interface;
using Flurl;

namespace Artesian.SDK.QueryService.Queries
{
    public class QueryService: IQueryService
    {
        private IArtesianServiceConfig _cfg;
        private Auth0Client _client;

        public QueryService(IArtesianServiceConfig cfg)
        {
            _cfg = cfg;
            _client = new Auth0Client(cfg, cfg.BaseAddress.ToString().AppendPathSegment(Constants.QueryRoute).AppendPathSegment(Constants.Version)
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
