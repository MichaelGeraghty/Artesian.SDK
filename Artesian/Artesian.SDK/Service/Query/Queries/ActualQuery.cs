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
    public class ActualQuery : Query, IActualQuery<ActualQuery>
    {
        protected Granularity? _granularity;
        private Auth0Client _client;
        protected int? _tr;
        private string _routePrefix = "ts";

        internal ActualQuery(Auth0Client client)
        {
            _client = client;
        }

        #region facade methods
        public ActualQuery ForMarketData(int[] ids)
        {
            _ids = ids;
            return this;
        }

        public ActualQuery ForMarketData(int id)
        {
            _ids = new int[] { id };
            return this;
        }

        public ActualQuery InTimezone(string tz)
        {
            _inTimezone(tz);
            return this;
        }

        public ActualQuery InAbsoluteDateRange(LocalDate start, LocalDate end)
        {
            _inAbsoluteDateRange(start, end);
            return this;
        }

        public ActualQuery InRelativePeriodRange(Period from, Period to)
        {
            _inRelativePeriodRange(from, to);
            return this;
        }

        public ActualQuery InRelativePeriod(Period extractionPeriod)
        {
            _inRelativePeriod(extractionPeriod);
            return this;
        }

        public ActualQuery InRelativeInterval(RelativeInterval relativeInterval)
        {
            _inRelativeInterval(relativeInterval);
            return this;
        }

        public ActualQuery WithTimeTransform(int tr)
        {
            _tr = tr;
            return this;
        }

        public ActualQuery WithTimeTransform(SystemTimeTransform tr)
        {
            _tr = (int)tr;
            return this;
        }
        #endregion


        #region actual query methods
        public ActualQuery InGranularity(Granularity granularity)
        {
            _granularity = granularity;
            return this;
        }

        public async Task<IEnumerable<TimeSerieRow.Actual>> ExecuteAsync(CancellationToken ctk = default)
        {
            return await _client.Exec<IEnumerable<TimeSerieRow.Actual>>(HttpMethod.Get, _buildRequest(), ctk: ctk);
        }


        #region private
        string _buildRequest()
        {
            _validateQuery();

            var url = $"/{_routePrefix}/{_granularity}/{_buildExtractionRangeRoute()}"
                .SetQueryParam("id", _ids)
                .SetQueryParam("tz", _tz)
                .SetQueryParam("tr", _tr);

            return url.ToString();
        }

        //not required if granularity set through ctor
        protected sealed override void _validateQuery()
        {
            base._validateQuery();

            if (_granularity == null)
                throw new ApplicationException("Extraction granularity must be provided");
        } 
        #endregion
        #endregion


    }
}
