using Artesian.SDK.Dependencies;
using NodaTime;

namespace Artesian.SDK.API.ArtesianService
{
    public class PeriodRange
    {
        public Period From { get; set; }
        public Period To { get; set; }
    }

    public class LastOfSelectionCfg
    {
        public LocalDateRange DateRange { get; set; }
        public Period Period { get; set; }
        public PeriodRange PeriodRange { get; set; }
    }

    public class VersionSelectioncfg
    {
        public int LastN { get; set; }
        public LocalDateTime Version { get; set; }
        public LastOfSelectionCfg LastOf { get; set; }
    }

    class ExtractionRangeSelectionCfg
    {
        public LocalDateRange DateRange { get; set; }
        public Period Period { get; set; }
        public PeriodRange PeriodRange { get; set; }
        public RelativeInterval Interval { get; set; }
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
