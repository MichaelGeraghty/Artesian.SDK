using MessagePack;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Artesian.SDK.Dependencies
{
    public class MessagePackFormatter : MediaTypeFormatter
    {
        private readonly IFormatterResolver _resolver;

        public static MediaTypeHeaderValue DefaultMediaType { get; } = new MediaTypeHeaderValue("application/x-msgpack");

        public MessagePackFormatter()
            : this(MessagePackSerializer.DefaultResolver)
        {
        }

        public MessagePackFormatter(IFormatterResolver resolver)
        {
            _resolver = resolver;
            SupportedMediaTypes.Add(DefaultMediaType);
        }

        public override bool CanReadType(Type type)
        {
            return _resolver.GetFormatterDynamic(type) != null;
        }

        public override bool CanWriteType(Type type)
        {
            return _resolver.GetFormatterDynamic(type) != null;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (writeStream == null)
            {
                throw new ArgumentNullException(nameof(writeStream));
            }

            MessagePackSerializer.NonGeneric.Serialize(type, writeStream, value, _resolver);
            return Task.FromResult(0);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (readStream == null)
            {
                throw new ArgumentNullException(nameof(readStream));
            }

            var value = MessagePackSerializer.NonGeneric.Deserialize(type, readStream, _resolver);
            return Task.FromResult(value);
        }
    }
}
