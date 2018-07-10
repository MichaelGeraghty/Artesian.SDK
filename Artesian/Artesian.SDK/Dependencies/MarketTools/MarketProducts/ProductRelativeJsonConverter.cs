using Newtonsoft.Json;
using System;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public class ProductRelativeJsonConverter : JsonConverter
    {
        private static readonly Type _nullableProductRelative = typeof(ProductRelative).IsValueType ? typeof(Nullable<>).MakeGenericType(typeof(ProductRelative)) : typeof(ProductRelative);
        public override bool CanConvert(Type objectType)
        {
            //return objectType == typeof(ProductRelative) || objectType == typeof(ProductRelative?);
            return typeof(ProductRelative).IsAssignableFrom(objectType) || objectType == _nullableProductRelative;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var productRelativeString = default(string);
            var productRelative = default(ProductRelative);
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType != _nullableProductRelative)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }

            productRelativeString = serializer.Deserialize<string>(reader);

            if (productRelativeString == null)
            {
                if (objectType != _nullableProductRelative)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }


            if (!ProductRelative.TryParse(productRelativeString, out productRelative))
            {
                throw new JsonReaderException(string.Format("The Product Relative is invalid: {0}", productRelativeString));
            }
            return productRelative;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            ProductRelative? productRelative = null;
            if (value is ProductRelative?)
                productRelative = (ProductRelative?)value;
            if (value is ProductRelative)
                productRelative = (ProductRelative)value;

            if (productRelative.HasValue)
                serializer.Serialize(writer, productRelative.Value.ToString());
            else
                writer.WriteNull();
        }
    }
}
