using Artesian.SDK.Dependencies.Common;
using Artesian.SDK.QueryService.Config;
using Artesian.SDK.QueryService.Configuration;
using NodaTime;
using System;
using System.Collections.Generic;

namespace Artesian.SDK.QueryService.Queries
{
    public abstract class Query
    {
        // must comment and document all methods
        private ExtractionRangeSelectionConfig _extractionRangeCfg = new ExtractionRangeSelectionConfig();
        private ExtractionRangeType? _extractionRangeType = null;

        protected IEnumerable<int> _ids;
        protected string _tz;

        protected Query _forMarketData(int[] ids)
        {
            _ids = ids;
            return this;
        }

        protected Query _inTimezone(string tz)
        {
            if (DateTimeZoneProviders.Tzdb.GetZoneOrNull(tz) == null)
                throw new ArgumentException($"Timezone {tz} is not recognized");
            _tz = tz;
            return this;
        }

        protected Query _inAbsoluteDateRange(LocalDateRange extractionDateRange)
        {
            _extractionRangeType = ExtractionRangeType.DateRange;
            _extractionRangeCfg.DateRange = extractionDateRange;
            return this;
        }

        protected Query _inRelativePeriodRange(PeriodRange extractionPeriodRange)
        {
            _extractionRangeType = ExtractionRangeType.PeriodRange;
            _extractionRangeCfg.PeriodRange = extractionPeriodRange;
            return this;
        }

        protected Query _inRelativePeriod(Period extractionPeriod)
        {
            _extractionRangeType = ExtractionRangeType.Period;
            _extractionRangeCfg.Period = extractionPeriod;
            return this;
        }

        protected Query _inRelativeInterval(RelativeInterval relativeInterval)
        {
            _extractionRangeType = ExtractionRangeType.RelativeInterval;
            _extractionRangeCfg.Interval = relativeInterval;
            return this;
        }

        protected string _buildExtractionRangeRoute()
        {
            string subPath;
            switch (_extractionRangeType)
            {
                case ExtractionRangeType.DateRange:
                    subPath = $"{UrlComposer.ToUrlParam(_extractionRangeCfg.DateRange)}";
                    break;
                case ExtractionRangeType.Period:
                    subPath = $"{_extractionRangeCfg.Period}";
                    break;
                case ExtractionRangeType.PeriodRange:
                    subPath = $"{_extractionRangeCfg.PeriodRange.From}/{_extractionRangeCfg.PeriodRange.To}";
                    break;
                case ExtractionRangeType.RelativeInterval:
                    subPath = $"{_extractionRangeCfg.Interval}";
                    break;
                default:
                    throw new Exception();
            }

            return subPath;
        }

        protected virtual void _validateQuery()
        {
            if (_extractionRangeType == null)
                throw new ApplicationException("Data extraction range must be provided");

            if (_ids == null)
                throw new ApplicationException("Marketadata ids must be provided for extraction");
        }

    }
}
