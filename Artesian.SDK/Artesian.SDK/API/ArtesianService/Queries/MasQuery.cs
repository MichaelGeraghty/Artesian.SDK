using Artesian.SDK.Common;
using Artesian.SDK.Dependencies;
using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using NodaTime;
using System;
using System.Collections.Generic;

namespace Artesian.SDK.API.ArtesianService.Queries
{
    class MasQuery : ArkiveQuery
    {
        private IEnumerable<IMarketProduct> _products;
        private string _routePrefix = "mas";

        public MasQuery(int[] ids)
        {
            _forMarketData(ids);
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

        protected override void _validateQuery()
        {
            base._validateQuery();

            if (_products == null)
                throw new ApplicationException("Products must be provided for extraction");
        }
        #endregion

        //public MasQuery ForMarketData(int[] ids)
        //{
        //    _ids = ids;
        //    return this;
        //}
    }
}
