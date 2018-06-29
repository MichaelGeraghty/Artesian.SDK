using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artesian.SDK.Common.Dto.Api.V2
{
    public enum MarketDataTypeV2
    {
        ActualTimeSerie
        , VersionedTimeSerie
        , MarketAssessment
    }

    public static class EnumExtension
    {
        public static MarketDataTypeV2 ToV2(this MarketDataType e)
        {
            MarketDataTypeV2 type = MarketDataTypeV2.ActualTimeSerie;

            switch (e)
            {
                case MarketDataType.ActualTimeSerie:
                    type = MarketDataTypeV2.ActualTimeSerie;
                    break;
                case MarketDataType.ForecastTimeSerie:
                    type = MarketDataTypeV2.VersionedTimeSerie;
                    break;
                case MarketDataType.MarketAssessment:
                    type = MarketDataTypeV2.MarketAssessment;
                    break;
            }

            return type;
        }
        public static MarketDataType ToV1(this MarketDataTypeV2 e)
        {
            MarketDataType type = MarketDataType.ActualTimeSerie;

            switch (e)
            {
                case MarketDataTypeV2.ActualTimeSerie:
                    type = MarketDataType.ActualTimeSerie;
                    break;
                case MarketDataTypeV2.VersionedTimeSerie:
                    type = MarketDataType.ForecastTimeSerie;
                    break;
                case MarketDataTypeV2.MarketAssessment:
                    type = MarketDataType.MarketAssessment;
                    break;
            }

            return type;
        }

        public static TEnum? ParseEnum<TEnum>(this string s)
        where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            if (Enum.TryParse<TEnum>(s, out var d))
            {
                return d;
            }

            return null;
        }
    }
}
