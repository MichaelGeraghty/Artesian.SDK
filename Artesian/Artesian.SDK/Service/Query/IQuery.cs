using NodaTime;

namespace Artesian.SDK.Service
{
    interface IQuery<T>
    {
        T ForMarketData(int[] ids);
        T InTimezone(string tz);
        T InAbsoluteDateRange(LocalDate from, LocalDate to);
        T InRelativePeriodRange(Period from, Period to);
        T InRelativePeriod(Period extractionPeriod);
        T InRelativeInterval(RelativeInterval relativeInterval);
    }
}