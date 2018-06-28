using Artesian.SDK.API.DTO;
using Artesian.SDK.Dependencies;
using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using NodaTime;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.API.ArtesianService
{
    public static class IArtesianQueryService
    {
        public interface Latest : V1_0 { }
        public interface Deprecated { }

        public interface V1_0
        {
            Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, LocalDateRange range, string tz = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, Period period, LocalDate? analysisDate = null, string tz = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default);
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDayRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDayRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonthRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonthRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, LocalDateRange range, IEnumerable<int> id, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, RelativeInterval interval, IEnumerable<int> id, LocalDate? analysisDate = null, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, Period period, IEnumerable<int> id, LocalDate? analysisDate = null, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, Period periodFrom, Period periodTo, IEnumerable<int> id, LocalDate? analysisDate = null, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken));

        }
    }
}
