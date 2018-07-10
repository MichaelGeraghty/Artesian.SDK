using Artesian.SDK.API.DTO;
using Artesian.SDK.Common.Dto.TimeTransform.Enum;
using System;
using System.Collections.Generic;

namespace Artesian.SDK.Common.Dto.TimeTransform.Dto.Hardcoded
{
    public static class HardcodedTimeTransform
    {
        public static TimeTransformSimpleShift GASDAY66 = new TimeTransformSimpleShift()
        {
            ID = 1,
            Name = HardCodedEnum.GASDAY66.ToString(),
            ETag = Guid.Empty,
            DefinedBy = TransformDefinitionType.System,
            Period = Granularity.Day,
            PositiveShift = "PT6H",
            NegativeShift = "",
        };

        public static TimeTransformSimpleShift THERMALYEAR = new TimeTransformSimpleShift()
        {
            ID = 2,
            Name = HardCodedEnum.THERMALYEAR.ToString(),
            ETag = Guid.Empty,
            DefinedBy = TransformDefinitionType.System,
            Period = Granularity.Year,
            PositiveShift = "",
            NegativeShift = "P3M",
        };

        public static List<TimeTransformBase> List = new List<TimeTransformBase>()
        {
            HardcodedTimeTransform.GASDAY66,
            HardcodedTimeTransform.THERMALYEAR,
        };
    }
}
