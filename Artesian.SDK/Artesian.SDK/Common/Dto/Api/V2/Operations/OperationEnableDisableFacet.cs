﻿using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Artesian.SDK.Common.Dto.Api.V2.Operations
{
    /// <summary>
    /// The OperationEnableFacet class.
    /// </summary>
    [MessagePackObject]
    public class OperationEnableDisableTag : IOperationParamsPayload
    {
        /// <summary>
        /// The Facet Name.
        /// </summary>
        [Required]
        [MessagePack.Key(0)]
        public string TagKey { get; set; }

        /// <summary>
        /// The Facet Value.
        /// </summary>
        [Required]
        [MessagePack.Key(1)]
        public string TagValue { get; set; }
    }
}
