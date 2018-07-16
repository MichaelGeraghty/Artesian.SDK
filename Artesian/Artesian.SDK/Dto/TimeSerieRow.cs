using MessagePack;
using Newtonsoft.Json;
using System;

namespace Artesian.SDK.Dto
{
    public static partial class TimeSerieRow
    {
        public static partial class Versioned
        {
            [MessagePackObject]
            public class V2
            {
                [JsonProperty(PropertyName = "P")]
                [Key(0)]
                public virtual string ProviderName { get; set; }

                [JsonProperty(PropertyName = "C")]
                [Key(1)]
                public virtual string CurveName { get; set; }

                [JsonProperty(PropertyName = "ID")]
                [Key(2)]
                public virtual int TSID { get; set; }

                [JsonProperty(PropertyName = "V")]
                [Key(3)]
                public virtual DateTime? Version { get; set; }

                [JsonProperty(PropertyName = "T")]
                [Key(4)]
                public virtual DateTimeOffset Time { get; set; }

                [JsonProperty(PropertyName = "D")]
                [Key(5)]
                public virtual double? Value { get; set; }
            }

            [MessagePackObject]
            public class V1_0 : V2
            {
                /// <summary>
                /// Start of first competence
                /// </summary>
                [JsonProperty(PropertyName = "S")]
                [Key(6)]
                public virtual DateTimeOffset CompetenceStart { get; set; }

                /// <summary>
                /// End of last competence
                /// </summary>
                [JsonProperty(PropertyName = "E")]
                [Key(7)]
                public virtual DateTimeOffset CompetenceEnd { get; set; }
            }
        }

        public static partial class Actual
        {
            [MessagePackObject]
            public class V2
            {
                /// <summary>
                /// The Provider display name
                /// </summary>
                [JsonProperty(PropertyName = "P")]
                [Key(0)]
                public virtual string ProviderName { get; set; }

                /// <summary>
                /// The Curve display name
                /// </summary>
                [JsonProperty(PropertyName = "C")]
                [Key(1)]
                public virtual string CurveName { get; set; }

                /// <summary>
                /// The Market Data ID
                /// </summary>
                [JsonProperty(PropertyName = "ID")]
                [Key(2)]
                public virtual int TSID { get; set; }

                /// <summary>
                /// The timestamp
                /// </summary>
                [JsonProperty(PropertyName = "T")]
                [Key(3)]
                public virtual DateTimeOffset Time { get; set; }

                /// <summary>
                /// The Value
                /// </summary>
                [JsonProperty(PropertyName = "D")]
                [Key(4)]
                public virtual double? Value { get; set; }
            }

            [MessagePackObject]
            public class V1_0 : V2
            {
                /// <summary>
                /// Start of first competence
                /// </summary>
                [JsonProperty(PropertyName = "S")]
                [Key(5)]
                public virtual DateTimeOffset CompetenceStart { get; set; }

                /// <summary>
                /// End of last competence
                /// </summary>
                [JsonProperty(PropertyName = "E")]
                [Key(6)]
                public virtual DateTimeOffset CompetenceEnd { get; set; }
            }
        }
    }
}
