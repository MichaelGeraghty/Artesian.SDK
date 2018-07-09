using Artesian.SDK.API.DTO;
using Artesian.SDK.Dependencies;
using NodaTime;
using System;

namespace Artesian.SDK.API.ArtesianService.Queries
{
    class ActualQuery : ArkiveQuery
    {
        protected Granularity? _granularity;
        protected int? _tr;
        private string _routePrefix = "ts";

        public ActualQuery(int[] ids, Granularity granularity)
        {
            _forMarketData(ids);
            _granularity = granularity;
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

            var url = new Common.UrlComposer($"/{_routePrefix}/{_granularity}/{_buildExtractionRangeRoute()}")
                    .AddQueryParam("id", _ids)
                    .AddQueryParam("tz", _tz)
                    .AddQueryParam("tr", _tr);

            return url.ToString();
        }

        //not required if granularity set through ctor
        protected override void _validateQuery()
        {
            base._validateQuery();

            if (_granularity == null)
                throw new ApplicationException("Extraction granularity must be provided");
        }
        #endregion


        //public ActualQuery ForMarketData(int[] ids)
        //{
        //    _forMarketData(ids);
        //    return this;
        //}

        //public ActualQuery InGranularity(Granularity granularity)
        //{
        //    _granularity = granularity;
        //    return this;
        //}


    }
}
