using MessagePack;
using Newtonsoft.Json;
using System;

namespace Artesian.SDK.Dto
{
    [MessagePackObject]
    [Union(0, typeof(TimeTransformSimpleShift))]
    [JsonConverter(typeof(TimeTransformConverter))]
    public abstract class TimeTransform
    {
        [Key("ID")]
        public int ID { get; set; }
        [Key("Name")]
        public string Name { get; set; }
        [Key("Etag")]
        public Guid ETag { get; set; }
        [Key("DefinedBy")]
        public TransformDefinitionType DefinedBy { get; set; }

        [IgnoreMember]
        public abstract TransformType Type { get; }

    }
}
