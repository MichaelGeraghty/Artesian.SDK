namespace Artesian.SDK.Dependencies.Common
{
    public abstract class ArtesianConstants
    {
        public const int CurveIDIntLength = 9;
        public const int CurveIDMin = 100000000;
        public const int CurveIDMax = 999999999;
        public const string CharacterValidatorRegEx = @"^[^'"",:;\s](?:(?:[^'"",:;\s]| )*[^'"",:;\s])?$";
        public const string MarketDataNameValidatorRegEx = @"^[^\s](?:(?:[^\s]| )*[^\s])?$";

        public const int UserDefinedTransformMin = 1000;

        public static string EnforceEmptyPayload = string.Empty;
    }
}
