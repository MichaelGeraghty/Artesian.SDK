using EnsureThat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public static class Extensions
    {
        public static JsonSerializerSettings ConfigureForProductAbsolute(this JsonSerializerSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            // Add our converters
            _addDefaultConverters<ProductAbsoluteJsonConverter>(settings.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            settings.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return settings;
        }

        public static JsonSerializer ConfigureForProductAbsolute(this JsonSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }
            // Add our converters
            _addDefaultConverters<ProductAbsoluteJsonConverter>(serializer.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            serializer.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return serializer;
        }

        public static JsonSerializerSettings ConfigureForProductRelative(this JsonSerializerSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            // Add our converters
            _addDefaultConverters<ProductRelativeJsonConverter>(settings.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            settings.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return settings;
        }

        public static JsonSerializer ConfigureForProductRelative(this JsonSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }
            // Add our converters
            _addDefaultConverters<ProductRelativeJsonConverter>(serializer.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            serializer.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return serializer;
        }

        public static JsonSerializerSettings ConfigureForProductSpecial(this JsonSerializerSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            // Add our converters
            _addDefaultConverters<ProductSpecialJsonConverter>(settings.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            settings.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return settings;
        }

        public static JsonSerializer ConfigureForProductSpecial(this JsonSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }
            // Add our converters
            _addDefaultConverters<ProductSpecialJsonConverter>(serializer.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            serializer.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return serializer;
        }

        public static JsonSerializerSettings ConfigureForIMarketProduct(this JsonSerializerSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            // Add our converters
            _addDefaultConverters<MarketProductJsonConverter>(settings.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            settings.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return settings;
        }

        public static JsonSerializer ConfigureForIMarketProduct(this JsonSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }
            // Add our converters
            _addDefaultConverters<MarketProductJsonConverter>(serializer.Converters);

            // Disable automatic conversion of anything that looks like a date and time to BCL types.
            serializer.DateParseHandling = DateParseHandling.None;

            // return to allow fluent chaining if desired
            return serializer;
        }

        public static JsonSerializer ConfigureForMarketProducts(this JsonSerializer serializer)
        {
            return serializer.ConfigureForIMarketProduct().ConfigureForProductAbsolute().ConfigureForProductRelative().ConfigureForProductSpecial();
        }

        public static JsonSerializerSettings ConfigureForMarketProducts(this JsonSerializerSettings settings)
        {
            return settings.ConfigureForIMarketProduct().ConfigureForProductAbsolute().ConfigureForProductRelative().ConfigureForProductSpecial();
        }

        private static void _addDefaultConverters<T>(IList<JsonConverter> converters) where T : JsonConverter, new()
        {
            EnsureArg.IsNotNull(converters);

            if (!converters.OfType<T>().Any())
                converters.Add(new T());
        }
    }
}
