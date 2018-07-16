using NodaTime;

namespace Artesian.SDK.Service
{
    class LastOfSelectionConfig
    {
        public LocalDateRange? DateRange { get; set; }
        public Period Period { get; set; }
        public PeriodRange PeriodRange { get; set; }
    }
}
