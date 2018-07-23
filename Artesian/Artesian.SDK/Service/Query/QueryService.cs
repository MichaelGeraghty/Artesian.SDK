using Flurl;

namespace Artesian.SDK.Service
{
    public class QueryService: IQueryService
    {
        private IArtesianServiceConfig _cfg;
        private Auth0Client _client;

        public QueryService(IArtesianServiceConfig cfg)
        {
            _cfg = cfg;
            _client = new Auth0Client(cfg, cfg.BaseAddress.ToString().AppendPathSegment(ArtesianConstants.QueryRoute).AppendPathSegment(ArtesianConstants.QueryVersion)
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
