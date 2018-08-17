// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack;
using System.Collections.Generic;

namespace Artesian.SDK.Dto
{
    [MessagePackObject]
    public class PagedResult<T>
    {
        [Key(0)]
        public int Page { get; set; }
        [Key(1)]
        public int PageSize { get; set; }
        [Key(2)]
        public long Count { get; set; }
        [Key(3)]
        public bool IsCountPartial { get; set; }
        [Key(4)]
        public IEnumerable<T> Data { get; set; }
    }
}
