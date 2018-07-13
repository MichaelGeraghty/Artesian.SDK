﻿using Artesian.SDK.Dependencies;
using Artesian.SDK.QueryService.Configuration;
using NodaTime;

namespace Artesian.SDK.QueryService.Config
{
    class LastOfSelectionConfig
    {
        public LocalDateRange? DateRange { get; set; }
        public Period Period { get; set; }
        public PeriodRange PeriodRange { get; set; }
    }
}