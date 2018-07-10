using Artesian.SDK.QueryService.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using System;

namespace Artesian.SDK.Dependencies.TimeTools.Json
{
    public class LocalDateTimeRangeConverter : JsonConverter
    {
        private static readonly Type _type = typeof(LocalDateTimeRange);
        private static readonly Type _nullableType = typeof(Nullable<LocalDateTimeRange>);

        public override bool CanConvert(Type objectType)
        {
            return objectType == _type || objectType == _nullableType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType != _nullableType)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }

            var jo = JObject.Load(reader);

            if (jo == null)
            {
                if (objectType != _nullableType)
                {
                    throw new JsonReaderException(string.Format("Cannot convert null value to {0}.", objectType));
                }
                return null;
            }

            var start = jo["Start"];
            var end = jo["End"];

            if (start == null || end == null)
                throw new JsonReaderException("Json string is not a LocalDateTimeRange");

            var startDate = start.ToObject(typeof(LocalDateTime), serializer);
            var endDate = end.ToObject(typeof(LocalDateTime), serializer);

            if (startDate == null)
                throw new JsonReaderException("Start field is not a valid LocalDateTime");
            if (endDate == null)
                throw new JsonReaderException("Start field is not a valid LocalDateTime");


            return new LocalDateTimeRange((LocalDateTime)startDate, (LocalDateTime)endDate);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (!(value is LocalDateTimeRange || value is Nullable<LocalDateTimeRange>))
            {
                throw new ArgumentException(string.Format("Unexpected value when converting. Expected {0}, got {1}.", typeof(LocalDateTimeRange).FullName, value.GetType().FullName));
            }

            LocalDateTimeRange? r = null;

            if (value is Nullable<LocalDateTimeRange>)
                r = value as Nullable<LocalDateTimeRange>;


            if (value is LocalDateTimeRange)
            {
                r = (LocalDateTimeRange)value;
            }

            if (r.HasValue)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Start");
                serializer.Serialize(writer, r.Value.Start);

                writer.WritePropertyName("End");
                serializer.Serialize(writer, r.Value.End);

                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNull();
            }


        }
    }
}
