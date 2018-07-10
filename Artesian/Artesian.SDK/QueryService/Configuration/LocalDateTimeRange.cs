using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artesian.SDK.QueryService.Configuration
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
