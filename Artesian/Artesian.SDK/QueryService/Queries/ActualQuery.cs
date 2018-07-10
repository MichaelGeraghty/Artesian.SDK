using Artesian.SDK.Clients;
using Artesian.SDK.Dependencies;
using Artesian.SDK.Dependencies.Common.DTO;
using Artesian.SDK.QueryService.Config;
using Artesian.SDK.QueryService.Interface;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Artesian.SDK.QueryService.Queries
{
    public class ActualQuery : ArkiveQuery, IActualQuery<ActualQuery>
    {
        protected Granularity? _granularity;
        private Auth0Client _client;
        protected int? _tr;
        private string _routePrefix = "ts";

        internal ActualQuery(int[] ids, Granularity granularity, Auth0Client client)
        {
            _forMarketData(ids);
            _granularity = granularity;
            _client = client;
        }

        #region facade methods
        public ActualQuery InTimezone(string tz)
        {
            _inTimezone(tz);
            return this;
        }

        public ActualQuery InAbsoluteDateRange(LocalDateRange extractionDateRange)
        {

            _inAbsoluteDateRange(extractionDateRange);
            return this;
        }

        public ActualQuery InRelativePeriodRange(PeriodRange extractionPeriodRange)
        {
            _inRelativePeriodRange(extractionPeriodRange);
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
        #endregion


        #region actual query methods
        public string Build()
        {
            _validateQuery();

            var url = new Dependencies.Common.UrlComposer($"/{_routePrefix}/{_granularity}/{_buildExtractionRangeRoute()}")
                    .AddQueryParam("id", _ids)
                    .AddQueryParam("tz", _tz)
                    .AddQueryParam("tr", _tr);

            return url.ToString();
        }

        public async Task<IEnumerable<TimeSerieRow.Actual.V1_0>> ExecuteAsync()
        {
            return await _client.Exec<IEnumerable<TimeSerieRow.Actual.V1_0>>(HttpMethod.Get, Build());
        }

        //not required if granularity set through ctor
        protected override void _validateQuery()
        {
            base._validateQuery();

            if (_granularity == null)
                throw new ApplicationException("Extraction granularity must be provided");
        }
        #endregion


    }
}
