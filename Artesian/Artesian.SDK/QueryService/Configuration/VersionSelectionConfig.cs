using NodaTime;

namespace Artesian.SDK.QueryService.Config
{
    class VersionSelectionConfig
    {
        public int LastN { get; set; }
        public LocalDateTime Version { get; set; }
        public LastOfSelectionConfig LastOf { get; set; }
    }
}
