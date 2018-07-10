using Artesian.SDK.Dependencies;
using Artesian.SDK.QueryService.Config;
using NodaTime;

namespace Artesian.SDK.QueryService.Interface
{
    interface IQuery<T>
    {
        T InTimezone(string tz);
        T InAbsoluteDateRange(LocalDateRange extractionDateRange);
        T InRelativePeriodRange(PeriodRange extractionPeriodRange);
        T InRelativePeriod(Period extractionPeriod);
        T InRelativeInterval(RelativeInterval relativeInterval);
    }
}
