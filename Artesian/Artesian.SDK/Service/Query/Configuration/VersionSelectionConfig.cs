// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using NodaTime;

namespace Artesian.SDK.Service
{
    class VersionSelectionConfig
    {
        public int LastN { get; set; }
        public LocalDateTime Version { get; set; }
        public LastOfSelectionConfig LastOf { get; set; } = new LastOfSelectionConfig();
    }
}
