using NodaTime;

namespace Artesian.SDK.Service
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
