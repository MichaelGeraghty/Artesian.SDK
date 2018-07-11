using Artesian.SDK.Dto;
using Artesian.SDK.QueryService.Config;
using Artesian.SDK.QueryService.Configuration;
using NodaTime;

namespace Artesian.SDK.QueryService.Interface
{
    interface IQuery<T>
    {
        T ForMarketData(int[] ids);
        T InTimezone(string tz);
        T InAbsoluteDateRange(LocalDateRange extractionDateRange);
        T InRelativePeriodRange(PeriodRange extractionPeriodRange);
        T InRelativePeriod(Period extractionPeriod);
        T InRelativeInterval(RelativeInterval relativeInterval);
    }
}
