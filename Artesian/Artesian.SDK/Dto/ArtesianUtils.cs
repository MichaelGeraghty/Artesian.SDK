// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using Artesian.SDK.Service;
using System;

namespace Artesian.SDK.Dto
{
    /// <summary>
    /// Artesian Utils class for validation
    /// </summary>
    public class ArtesianUtils
    {
        /// <summary>
        /// Is valid string checks to see if the string provided is between the provided min and max lenght
        /// </summary>
        /// <param name="validStringCheck">string</param>
        /// <param name="minLenght">int</param>
        /// <param name="maxLenght">int</param>
        public static void IsValidString(string validStringCheck, int minLenght, int maxLenght)
        {
            if (String.IsNullOrEmpty(validStringCheck))
                throw new ArgumentException("Provider null or empty exception");
            if (validStringCheck.Length < minLenght || validStringCheck.Length > maxLenght)
                throw new Exception("Provider must be between 1 and 50 characters.");
            if (validStringCheck.Equals(ArtesianConstants.CharacterValidatorRegEx))
                throw new Exception("Invalid string. Should not contains trailing or leading whitespaces or any of the following characters: ,:;'\"<space>");
        }
        /// <summary>
        /// Is valid provider
        /// </summary>
        /// <param name="provider">string</param>
        /// <param name="minLenght">int</param>
        /// <param name="maxLenght">int</param>
        public static void IsValidProvider(string provider, int minLenght, int maxLenght)
        {
            IsValidString(provider, minLenght, maxLenght);
        }
        /// <summary>
        /// Is valid Market Data name
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="minLenght">int</param>
        /// <param name="maxLenght">int</param>
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
