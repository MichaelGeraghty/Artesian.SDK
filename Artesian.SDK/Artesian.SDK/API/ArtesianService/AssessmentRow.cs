﻿using MessagePack;
using Newtonsoft.Json;
using System;

namespace Artesian.SDK.API.ArtesianService
{
    public static partial class AssessmentRow
    {
        [MessagePackObject]
        public class V2
        {
            /// <summary>
            /// Provider Name
            /// </summary>
            [JsonProperty(PropertyName = "P")]
            [Key(0)]
            public virtual string ProviderName { get; set; }

            /// <summary>
            /// Curve Name
            /// </summary>
            [JsonProperty(PropertyName = "N")]
            [Key(1)]
            public virtual string CurveName { get; set; }

            /// <summary>
            /// Market Data ID
            /// </summary>
            [JsonProperty(PropertyName = "ID")]
            [Key(2)]
            public virtual int TSID { get; set; }

            /// <summary>
            /// Product Name
            /// </summary>
            [JsonProperty(PropertyName = "PR")]
            [Key(3)]
            public virtual string Product { get; set; }

            /// <summary>
            /// Timestamp
            /// </summary>
            [JsonProperty(PropertyName = "T")]
            [Key(4)]
            public virtual DateTimeOffset Time { get; set; }

            #region Mas Values
            /// <summary>
            /// Settlement
            /// </summary>
            [JsonProperty(PropertyName = "S")]
            [Key(5)]
            public double? Settlement { get; set; }

            /// <summary>
            /// Open
            /// </summary>
            [JsonProperty(PropertyName = "O")]
            [Key(6)]
            public double? Open { get; set; }

            /// <summary>
            /// Close
            /// </summary>
            [JsonProperty(PropertyName = "C")]
            [Key(7)]
            public double? Close { get; set; }

            /// <summary>
            /// High
            /// </summary>
            [JsonProperty(PropertyName = "H")]
            [Key(8)]
            public double? High { get; set; }

            /// <summary>
            /// Low
            /// </summary>
            [JsonProperty(PropertyName = "L")]
            [Key(9)]
            public double? Low { get; set; }

            /// <summary>
            /// Volume Paid
            /// </summary>
            [JsonProperty(PropertyName = "VP")]
            [Key(10)]
            public double? VolumePaid { get; set; }

            /// <summary>
            /// Volume Given
            /// </summary>
            [JsonProperty(PropertyName = "VG")]
            [Key(11)]
            public double? VolumeGiven { get; set; }

            /// <summary>
            /// Volume Total
            /// </summary>
            [JsonProperty(PropertyName = "VT")]
            [Key(12)]
            public double? VolumeTotal { get; set; }

            #endregion Mas Values
        }
    }
}
