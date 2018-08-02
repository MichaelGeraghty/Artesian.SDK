// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
namespace Artesian.SDK.Service
{
    public abstract class ArtesianConstants
    {
        internal const string CharacterValidatorRegEx = @"^[^'"",:;\s](?:(?:[^'"",:;\s]| )*[^'"",:;\s])?$";
        internal const string MarketDataNameValidatorRegEx = @"^[^\s](?:(?:[^\s]| )*[^\s])?$";
        internal static string QueryVersion { get { return "v1.0"; } }
        internal static string QueryRoute { get { return "query"; } }
        internal static string MetadataVersion { get { return "v2.1"; } }

    }
}
