using Newtonsoft.Json;
using System;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public class ProductAbsoluteJsonConverter : JsonConverter
    {
        private static readonly Type _nullableProductAbsolute = typeof(ProductAbsolute).IsValueType ? typeof(Nullable<>).MakeGenericType(typeof(ProductAbsolute)) : typeof(ProductAbsolute);
        public override bool CanConvert(Type objectType)
        {
            //return objectType == typeof(ProductAbsolute) || objectType == typeof(ProductAbsolute?);
            return typeof(ProductAbsolute).IsAssignableFrom(objectType) || objectType == _nullableProductAbsolute;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var productAbsoluteString = default(string);
            var productAbsolute = default(ProductAbsolute);
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType != _nullableProductAbsolute)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }

            productAbsoluteString = serializer.Deserialize<string>(reader);

            if (productAbsoluteString == null)
            {
                if (objectType != _nullableProductAbsolute)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }


            if (!ProductAbsolute.TryParse(productAbsoluteString, out productAbsolute))
            {
                throw new JsonReaderException(string.Format("The Product absolute is invalid: {0}", productAbsoluteString));
            }
            return productAbsolute;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            ProductAbsolute? productAbsolute = null;
            if (value is ProductAbsolute?)
                productAbsolute = (ProductAbsolute?)value;
            if (value is ProductAbsolute)
                productAbsolute = (ProductAbsolute)value;

            if (productAbsolute.HasValue)
                serializer.Serialize(writer, productAbsolute.Value.ToString());
            else
                writer.WriteNull();
        }
    }
}
