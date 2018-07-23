using NodaTime;

namespace Artesian.SDK.Service
{
    internal class PeriodRange
    {
        internal PeriodRange(Period from, Period to)
        {
            From = from;
            To = to;
        }

        public Period From { get; set; }
        public Period To { get; set; }
    }
}
