using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artesian.SDK.QueryService.Configuration
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
