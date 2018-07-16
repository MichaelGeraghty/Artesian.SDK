using NodaTime;

namespace Artesian.SDK.Service
{
    public struct LocalDateRange
    {
        public LocalDateRange(LocalDate start, LocalDate end)
        {
            Start = start;
            End = end;
        }
        public LocalDate Start { get; set; }
        public LocalDate End { get; set; }
    }
}
