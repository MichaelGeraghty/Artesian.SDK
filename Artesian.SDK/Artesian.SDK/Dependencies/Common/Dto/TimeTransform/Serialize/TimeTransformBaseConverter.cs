using Artesian.SDK.Common.Dto.TimeTransform.Dto;
using Artesian.SDK.Common.Dto.TimeTransform.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Artesian.SDK.Common.Dto.TimeTransform.Serialize
{
    class TimeTransformBaseConverter : JsonCreationConverter<TimeTransformBase>
    {
        protected override TimeTransformBase Create(Type objectType, JObject jObject)
        {

            if (jObject.TryGetValue(nameof(TimeTransformBase.Type), StringComparison.InvariantCultureIgnoreCase, out var token))
            {
                if (token.ToObject<TransformType>() == TransformType.SimpleShift)
                {
                    return new TimeTransformSimpleShift();
                }
                else if (token.ToObject<TransformType>() == TransformType.Calendar)
                {
                    throw new NotImplementedException($@"Not yet impletemented transform {TransformType.Calendar}");
                }
                else if (token.ToObject<TransformType>() == TransformType.Composition)
                {
                    throw new NotImplementedException($@"Not yet impletemented transform {TransformType.Composition}");
                }

                throw new InvalidOperationException("Can't deserialize TimeTransformBase. TransformType field not valid.");
            }

            throw new InvalidOperationException("Can't deserialize TimeTransformBase. TransformType field not found.");
        }
    }

    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
