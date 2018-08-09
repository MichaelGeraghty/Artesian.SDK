// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using NodaTime;

namespace Artesian.SDK.Service
{
    /// <summary>
    /// Version selection configuration
    /// </summary>
    public class VersionSelectionConfig
    {
        /// <summary>
        /// LastN version
        /// </summary>
        public int LastN { get; set; }
        /// <summary>
        /// Local date time version
        /// </summary>
        public LocalDateTime Version { get; set; }
        /// <summary>
        /// last of version <see cref="LastOfSelectionConfig"/>
        /// </summary>
        public LastOfSelectionConfig LastOf { get; set; } = new LastOfSelectionConfig();
    }
}
