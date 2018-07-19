using Newtonsoft.Json;
using NodaTime.Serialization.JsonNet;
using System;
using System.Collections.Generic;

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

            // Add our converters
            AddDefaultConverters(settings.Converters);

            // return to allow fluent chaining if desired
            return settings;
        }

        public static JsonSerializer ConfigureForNodaTimeRanges(this JsonSerializer serializer)
        {
            if(serializer==null)
                throw new ArgumentNullException("ConfigureForNodaTimeRanges null serializer exception");


            if (!serializer.Converters.Any(x => x == NodaConverters.LocalDateConverter))
                throw new InvalidOperationException("Missing NodaTime converters. Call 'ConfigureForNodaTime()' before 'ConfigureForNodaTimeRanges()'");

            // Add our converters
            AddDefaultConverters(serializer.Converters);

            // return to allow fluent chaining if desired
            return serializer;
        }

        private static void AddDefaultConverters(IList<JsonConverter> converters)
        {
            if (converters == null)
                throw new ArgumentNullException("AddDefaultConverters converters null exception");

            converters.Add(new LocalDateRangeConverter());
            converters.Add(new LocalDateTimeRangeConverter());
        }
    }
}
