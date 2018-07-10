using System;
using System.Collections.Generic;
using System.Text;

namespace Artesian.SDK.API.ArtesianService.Config
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
}
