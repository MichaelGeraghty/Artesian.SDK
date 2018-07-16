using NodaTime;

namespace Artesian.SDK.Service
{
    public struct LocalDateTimeRange
    {
        public LocalDateTimeRange(LocalDateTime start, LocalDateTime end)
        {
            Start = start;
            End = end;
        }
        public LocalDateTime Start { get; set; }
        public LocalDateTime End { get; set; }
    }
}
