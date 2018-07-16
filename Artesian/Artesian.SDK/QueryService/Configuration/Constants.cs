namespace Artesian.SDK.QueryService.Config
{
    class Constants
    {
    }

    enum ExtractionType
    {
        Actual,
        Versioned,
        MAS
    }

    enum ExtractionRangeType
    {
        DateRange,
        Period,
        PeriodRange,
        RelativeInterval
    }

    enum VersionSelectionType
    {
        LastN,
        MUV,
        LastOfDays,
        LastOfMonths,
        Version
    }

    public enum SystemTimeTransform
    {
        GASDAY66 = 1,
        THERMALYEAR = 2,
    }
}
