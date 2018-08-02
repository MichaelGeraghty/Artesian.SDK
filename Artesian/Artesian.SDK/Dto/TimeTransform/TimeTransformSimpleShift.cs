// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack;

namespace Artesian.SDK.Dto
{
    [MessagePackObject]
    public class TimeTransformSimpleShift : TimeTransform
    {
        [Key("Period")]
        public Granularity Period { get; set; }
        [Key(">")]
        public string PositiveShift { get; set; }
        [Key("<")]
        public string NegativeShift { get; set; }

        [IgnoreMember]
        public override TransformType Type => TransformType.SimpleShift;
    }
}
