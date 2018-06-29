using Newtonsoft.Json;
using System;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public class ProductSpecialJsonConverter : JsonConverter
    {
        private static readonly Type _nullableProductSpecial = typeof(ProductSpecial).IsValueType ? typeof(Nullable<>).MakeGenericType(typeof(ProductSpecial)) : typeof(ProductSpecial);
        public override bool CanConvert(Type objectType)
        {
            //return objectType == typeof(ProductSpecial) || objectType == typeof(ProductSpecial?);
            return typeof(ProductSpecial).IsAssignableFrom(objectType) || objectType == _nullableProductSpecial;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var productSpecialString = default(string);
            var productSpecial = default(ProductSpecial);
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType != _nullableProductSpecial)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }

            productSpecialString = serializer.Deserialize<string>(reader);

            if (productSpecialString == null)
            {
                if (objectType != _nullableProductSpecial)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }


            if (!ProductSpecial.TryParse(productSpecialString, out productSpecial))
            {
                throw new JsonReaderException(string.Format("The Product Special is invalid: {0}", productSpecialString));
            }
            return productSpecial;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            ProductSpecial? productSpecial = null;
            if (value is ProductSpecial?)
                productSpecial = (ProductSpecial?)value;
            if (value is ProductSpecial)
                productSpecial = (ProductSpecial)value;

            if (productSpecial.HasValue)
                serializer.Serialize(writer, productSpecial.Value.ToString());
            else
                writer.WriteNull();
        }
    }
}
