// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using Newtonsoft.Json;
using NodaTime.Serialization.JsonNet;
using System;
using System.Linq;

namespace Artesian.SDK.Service
{
    internal static class TimeToolsExtensions
    {
        public static JsonSerializerSettings ConfigureForNodaTimeRanges(this JsonSerializerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("ConfigureForNodaTimeRanges null settings exception");


            if (!settings.Converters.Any(x => x == NodaConverters.LocalDateConverter))
                throw new InvalidOperationException("Missing NodaTime converters. Call 'ConfigureForNodaTime()' before 'ConfigureForNodaTimeRanges()'");

            // return to allow fluent chaining if desired
            return settings;
        }

        public static JsonSerializer ConfigureForNodaTimeRanges(this JsonSerializer serializer)
        {
            if(serializer==null)
                throw new ArgumentNullException("ConfigureForNodaTimeRanges null serializer exception");


            if (!serializer.Converters.Any(x => x == NodaConverters.LocalDateConverter))
                throw new InvalidOperationException("Missing NodaTime converters. Call 'ConfigureForNodaTime()' before 'ConfigureForNodaTimeRanges()'");

            // return to allow fluent chaining if desired
            return serializer;
        }
    }
}
