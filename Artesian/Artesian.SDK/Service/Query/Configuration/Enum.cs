// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
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
    /// <summary>
    /// Relative interval enums
    /// </summary>
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
