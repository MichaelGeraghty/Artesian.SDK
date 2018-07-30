using NodaTime;

namespace Artesian.SDK.Service
{
    class LastOfSelectionConfig
    {
        public LocalDate? DateStart { get; set; }
        public LocalDate? DateEnd { get; set; }
        public Period Period { get; set; }
        public PeriodRange PeriodRange { get; set; }
    }
}
