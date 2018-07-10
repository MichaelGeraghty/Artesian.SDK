using Artesian.SDK.Dependencies;
using NodaTime;

namespace Artesian.SDK.API.ArtesianService.Config
{
    class LastOfSelectionConfig
    {
        public LocalDateRange DateRange { get; set; }
        public Period Period { get; set; }
        public PeriodRange PeriodRange { get; set; }
    }
}
