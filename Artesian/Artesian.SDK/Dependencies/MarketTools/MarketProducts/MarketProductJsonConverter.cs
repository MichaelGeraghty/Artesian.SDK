using Newtonsoft.Json;
using System;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public class MarketProductJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //return objectType == typeof(IMarketProduct) || objectType == typeof(IMarketProduct?);
            return typeof(IMarketProduct) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var marketProductString = default(string);
            var marketProduct = default(IMarketProduct);
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            marketProductString = serializer.Deserialize<string>(reader);

            if (marketProductString == null)
            {
                return null;
            }


            if (!MarketProductBuilder.TryParse(marketProductString, out marketProduct))
            {
                throw new JsonReaderException(string.Format("The Product is invalid: {0}", marketProductString));
            }
            return marketProduct;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {

                serializer.Serialize(writer, ((IMarketProduct)value).ToString());
            }
        }


    }
}
