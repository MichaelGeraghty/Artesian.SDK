// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack;

namespace Artesian.SDK.Dto
{
    [MessagePackObject]
    public class MarketAssessmentValue
    {
        public MarketAssessmentValue(
            double? settlement = null,
            double? open = null,
            double? close = null,
            double? high = null,
            double? low = null,
            double? volumePaid = null,
            double? volumeGiven = null,
            double? volume = null)
        {
            Settlement = settlement;
            Open = open;
            Close = close;
            High = high;
            Low = low;
            VolumePaid = volumePaid;
            VolumeGiven = volumeGiven;
            Volume = volume;
        }
        [Key(0)]
        public double? Settlement { get; set; }
        [Key(1)]
        public double? Open { get; set; }
        [Key(2)]
        public double? Close { get; set; }
        [Key(3)]
        public double? High { get; set; }
        [Key(4)]
        public double? Low { get; set; }
        [Key(5)]
        public double? VolumePaid { get; set; }
        [Key(6)]
        public double? VolumeGiven { get; set; }
        [Key(7)]
        public double? Volume { get; set; }
    }
}
