using Artesian.SDK.API.Dto.Api.V2.CurveData;
using Artesian.SDK.API.DTO;
using Artesian.SDK.Dependencies;
using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using NodaTime;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.API.ArtesianService.Data
{
    public class IArtesianDataService
    {
        public interface V2_0
        {
            // WriteAPI
            Task UpsertCurveDataAsync(UpsertCurveData data, CancellationToken ctk = default(CancellationToken));

            // HomogeneusAPI
            Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Actual.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Actual.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
        }

        public interface V2_1
        {
            // WriteAPI
            Task UpsertCurveDataAsync(UpsertCurveData data, CancellationToken ctk = default(CancellationToken));

            // HomogeneusAPI
            Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken));
            Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken));
        }

    }
}

