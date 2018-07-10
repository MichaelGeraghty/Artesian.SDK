using Artesian.SDK.API.ArtesianService.Config;
using Artesian.SDK.API.ArtesianService.Interface;
using Artesian.SDK.ArtesianService.Clients;
using Artesian.SDK.Common;
using Artesian.SDK.Dependencies;
using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Artesian.SDK.API.ArtesianService.Queries
{
    public class MasQuery : ArkiveQuery, IMasQuery<MasQuery>
    {
        private IEnumerable<IMarketProduct> _products;
        private string _routePrefix = "mas";
        private Auth0Client _client;

        internal MasQuery(int[] ids, Auth0Client client)
        {
            _forMarketData(ids);
            _client = client;
        }

        #region facade methods
        public MasQuery InTimezone(string tz)
        {
            _inTimezone(tz);
            return this;
        }

        public MasQuery InAbsoluteDateRange(LocalDateRange extractionDateRange)
        {
            _inAbsoluteDateRange(extractionDateRange);
            return this;
        }

        public MasQuery InRelativePeriodRange(PeriodRange extractionPeriodRange)
        {
            _inRelativePeriodRange(extractionPeriodRange);
            return this;
        }

        public MasQuery InRelativePeriod(Period extractionPeriod)
        {
            _inRelativePeriod(extractionPeriod);
            return this;
        }

        public MasQuery InRelativeInterval(RelativeInterval relativeInterval)
        {
            _inRelativeInterval(relativeInterval);
            return this;
        }
        #endregion

        #region market assessment methods

        public MasQuery ForProducts(IEnumerable<IMarketProduct> products)
        {
            _products = products;
            return this;
        }

        public string Build()
        {
            _validateQuery();

            var url = new UrlComposer($"/{_routePrefix}/{_buildExtractionRangeRoute()}")
            .AddQueryParam("id", _ids)
            .AddQueryParam("p", _products)
            .AddQueryParam("tz", _tz);

            return url.ToString();
        }

        public async Task<IEnumerable<AssessmentRow.V2>> ExecuteAsync()
        {
            return await _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, Build());
        }

        protected override void _validateQuery()
        {
            base._validateQuery();

            if (_products == null)
                throw new ApplicationException("Products must be provided for extraction");
        }
        #endregion
    }
}
