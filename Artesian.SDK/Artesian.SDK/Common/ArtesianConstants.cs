using System;
using System.Collections.Generic;
namespace Artesian.SDK.Common
{
    public abstract class ArtesianConstants
    {
        public const int CurveIDIntLength = 9;
        public const int CurveIDMin = 100000000;
        public const int CurveIDMax = 999999999;
        public const string CharacterValidatorRegEx = @"^[^'"",:;\s](?:(?:[^'"",:;\s]| )*[^'"",:;\s])?$";
        public const string MarketDataNameValidatorRegEx = @"^[^\s](?:(?:[^\s]| )*[^\s])?$";

        public const int UserDefinedTransformMin = 1000;

        public const string ArtesianAssemblyVersion = "3.3";
        public const string ArtesianFileVersion = "3.3.19";

        public static string EnforceEmptyPayload = string.Empty;
    }
}
