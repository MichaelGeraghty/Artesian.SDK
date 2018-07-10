using Artesian.SDK.API.DTO;
using Artesian.SDK.Common.Dto.TimeTransform.Enum;
using MessagePack;

namespace Artesian.SDK.Common.Dto.TimeTransform.Dto
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
