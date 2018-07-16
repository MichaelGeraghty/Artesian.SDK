namespace Artesian.SDK.Service
{
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

    public enum RelativeInterval
    {
        RollingWeek,
        RollingMonth,
        RollingQuarter,
        RollingYear,
        WeekToDate,
        MonthToDate,
        QuarterToDate,
        YearToDate
    }
}
