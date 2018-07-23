using NodaTime;

namespace Artesian.SDK.Service
{
    internal struct LocalDateRange
    {
        internal LocalDateRange(LocalDate start, LocalDate end)
        {
            Start = start;
            End = end;
        }
        public LocalDate Start { get; set; }
        public LocalDate End { get; set; }
    }
}
