using Artesian.SDK.API.ArtesianService.Config;
using Artesian.SDK.Dependencies;
using NodaTime;

namespace Artesian.SDK.API.ArtesianService.Interface
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
