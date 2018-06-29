using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using System;

namespace Artesian.SDK.Dependencies.TimeTools.Json
{
    public class ZonedDateTimeRangeConverter : JsonConverter
    {
        private static readonly Type _type = typeof(ZonedDateTimeRange);
        private static readonly Type _nullableType = typeof(Nullable<ZonedDateTimeRange>);

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
                throw new JsonReaderException("Json string is not a ZonedDateTimeRange");

            var startDate = start.ToObject(typeof(ZonedDateTime), serializer);
            var endDate = end.ToObject(typeof(ZonedDateTime), serializer);

            if (startDate == null)
                throw new JsonReaderException("Start field is not a valid ZonedDateTime");
            if (endDate == null)
                throw new JsonReaderException("Start field is not a valid ZonedDateTime");


            return new ZonedDateTimeRange((ZonedDateTime)startDate, (ZonedDateTime)endDate);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (!(value is ZonedDateTimeRange || value is Nullable<ZonedDateTimeRange>))
            {
                throw new ArgumentException(string.Format("Unexpected value when converting. Expected {0}, got {1}.", typeof(ZonedDateTimeRange).FullName, value.GetType().FullName));
            }

            ZonedDateTimeRange? r = null;

            if (value is Nullable<ZonedDateTimeRange>)
                r = value as Nullable<ZonedDateTimeRange>;


            if (value is ZonedDateTimeRange)
            {
                r = (ZonedDateTimeRange)value;
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
