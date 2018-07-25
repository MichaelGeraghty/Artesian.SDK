using Artesian.SDK.Dto;
using Flurl;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.Service
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

        public MasQuery ForMarketData(int id)
        {
            _ids = new int[] { id };
            return this;
        }

        public MasQuery InTimezone(string tz)
        {
            _inTimezone(tz);
            return this;
        }

        public MasQuery InAbsoluteDateRange(LocalDate start, LocalDate end)
        {
            _inAbsoluteDateRange(start, end);
            return this;
        }

        public MasQuery InRelativePeriodRange(Period from, Period to)
        {
            _inRelativePeriodRange(from, to);
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

        public async Task<IEnumerable<AssessmentRow>> ExecuteAsync(CancellationToken ctk = default)
        {
            return await _client.Exec<IEnumerable<AssessmentRow>>(HttpMethod.Get, _buildRequest(), ctk: ctk);
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