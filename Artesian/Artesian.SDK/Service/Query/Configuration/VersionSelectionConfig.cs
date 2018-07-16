using NodaTime;

namespace Artesian.SDK.Service
{
    class VersionSelectionConfig
    {
        public int LastN { get; set; }
        public LocalDateTime Version { get; set; }
        public LastOfSelectionConfig LastOf { get; set; }
    }
}
