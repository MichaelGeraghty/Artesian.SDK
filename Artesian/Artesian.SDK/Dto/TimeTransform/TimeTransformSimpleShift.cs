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
