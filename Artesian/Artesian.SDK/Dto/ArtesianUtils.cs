// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using Artesian.SDK.Service;
using System;

namespace Artesian.SDK.Dto
{
    public class ArtesianUtils
    {
        public static void IsValidString(string validStringCheck, int minLenght, int maxLenght)
        {
            if (String.IsNullOrEmpty(validStringCheck))
                throw new ArgumentException("Provider null or empty exception");
            if (validStringCheck.Length < minLenght || validStringCheck.Length > maxLenght)
                throw new Exception("Provider must be between 1 and 50 characters.");
            if (validStringCheck.Equals(ArtesianConstants.CharacterValidatorRegEx))
                throw new Exception("Invalid string. Should not contains trailing or leading whitespaces or any of the following characters: ,:;'\"<space>");
        }

        public static void IsValidProvider(string provider, int minLenght, int maxLenght)
        {
            IsValidString(provider, minLenght, maxLenght);
        }

        public static void IsValidMarketDataName(string name, int minLenght, int maxLenght)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Provider null or empty exception");
            if (name.Length < minLenght || name.Length > maxLenght)
                throw new Exception("Provider must be between 1 and 250 characters.");
            if (name.Equals(ArtesianConstants.CharacterValidatorRegEx))
                throw new Exception("Invalid string. Should not contains trailing or leading whitespaces or any of the following characters: ,:;'\"<space>");
        }
    }
}
