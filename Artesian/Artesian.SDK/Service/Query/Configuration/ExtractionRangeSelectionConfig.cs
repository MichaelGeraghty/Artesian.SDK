// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using NodaTime;

namespace Artesian.SDK.Service
{
    class ExtractionRangeSelectionConfig
    {
        public LocalDate DateStart{ get; set; }
        public LocalDate DateEnd { get; set; }
        public Period Period { get; set; }
        public Period PeriodFrom { get; set; }
        public Period PeriodTo { get; set; }
        public RelativeInterval Interval { get; set; }
    }
}
