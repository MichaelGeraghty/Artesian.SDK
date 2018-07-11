using MessagePack;
using MessagePack.Formatters;
using MessagePack.NodaTime;
using MessagePack.Resolvers;

namespace Artesian.SDK.Clients.Formatters
{
    public class CustomCompositeResolver : IFormatterResolver
    {
        public static IFormatterResolver Instance = new CustomCompositeResolver();

        static readonly IFormatterResolver[] resolvers = new[]
        {
            BuiltinResolver.Instance,
            NodatimeResolver.Instance,
            AttributeFormatterResolver.Instance,
            DynamicEnumAsStringResolver.Instance,
            StandardResolver.Instance
        };

        CustomCompositeResolver()
        {
        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                foreach (var item in resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        return;
                    }
                }
            }
        }
    }
}
