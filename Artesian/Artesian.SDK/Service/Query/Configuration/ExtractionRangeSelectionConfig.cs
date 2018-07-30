using NodaTime;

namespace Artesian.SDK.Service
{
    class ExtractionRangeSelectionConfig
    {
        public LocalDate DateStart{ get; set; }
        public LocalDate DateEnd { get; set; }
        public Period Period { get; set; }
        public Period PeriodFrom { get; set; }
        public Period PeriodTo { get; set; }
        public RelativeInterval Interval { get; set; }
    }
}
