using Artesian.SDK.Clients;
using Artesian.SDK.QueryService.Config;
using Artesian.SDK.QueryService.Configuration;
using Artesian.SDK.QueryService.Interface;
using Flurl;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Artesian.SDK.QueryService.Queries
{
    public class MasQuery : Query, IMasQuery<MasQuery>
    {
        private IEnumerable<string> _products;
        private string _routePrefix = "mas";
        private Auth0Client _client;

        internal MasQuery(Auth0Client client)
        {
            _client = client;
        }

        #region facade methods
        public MasQuery ForMarketData(int[] ids)
        {
            _ids = ids;
            return this;
        }

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

        public MasQuery ForProducts(params string[] products)
        {
            _products = products;
            return this;
        }

        public async Task<IEnumerable<AssessmentRow.V2>> ExecuteAsync()
        {
            return await _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, _buildRequest());
        }

        #region private
        string _buildRequest()
        {
            _validateQuery();

            var url = $"/{_routePrefix}/{_buildExtractionRangeRoute()}"
            .SetQueryParam("id", _ids)
            .SetQueryParam("p", _products)
            .SetQueryParam("tz", _tz);

            return url.ToString();
        }

        protected override void _validateQuery()
        {
            base._validateQuery();

            if (_products == null)
                throw new ApplicationException("Products must be provided for extraction");
        } 
        #endregion
        #endregion
    }
}
