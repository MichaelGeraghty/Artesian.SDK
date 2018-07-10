using Artesian.SDK.Dependencies.Common.Dto.TimeTransform.Enum;
using Artesian.SDK.Dependencies.Common.DTO;
using MessagePack;

namespace Artesian.SDK.Dependencies.Common.Dto.TimeTransform.Dto
{
    [MessagePackObject]
    public class TimeTransformSimpleShift : TimeTransformBase
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
