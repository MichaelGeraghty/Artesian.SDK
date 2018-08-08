// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using Flurl;

namespace Artesian.SDK.Service
{
    public class QueryService: IQueryService
    {
        private IArtesianServiceConfig _cfg;
        private Client _client;

        public QueryService(IArtesianServiceConfig cfg)
        {
            _cfg = cfg;
            _client = new Client(cfg, cfg.BaseAddress.ToString().AppendPathSegment(ArtesianConstants.QueryRoute).AppendPathSegment(ArtesianConstants.QueryVersion)
            );
        }
        /// <summary>
        /// Create Actual Time Serie
        /// </summary>
        /// <returns>
        /// Actual Time Serie <see cref="ActualQuery"/>
        /// </returns>
        public ActualQuery CreateActual()
        {
            return new ActualQuery(_client);
        }
        /// <summary>
        /// Create Versioned Time Serie
        /// </summary>
        /// <returns>
        /// Versioned Time Serie <see cref="VersionedQuery"/>
        /// </returns>
        public VersionedQuery CreateVersioned()
        {
            return new VersionedQuery(_client);
        }
        /// <summary>
        /// Create Market Assessment Time Serie
        /// </summary>
        /// <returns>
        /// Market Assessment Time Serie <see cref="MasQuery"/>
        /// </returns>
        public MasQuery CreateMarketAssessment()
        {
            return new MasQuery(_client);
        }
    }
}