using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace WordApp.Mongo
{
    public delegate bool GetSerializerFn(Type type, [NotNullWhen(true)] out IBsonSerializer? serializer);

    public class BsonSerializationProvider : List<GetSerializerFn>, IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            foreach (GetSerializerFn item in this)
                if (item(type, out IBsonSerializer? qq))
                    return qq;

            return null!;
        }
        private static readonly Lazy<BsonSerializationProvider> lazy_singleInstance = new(() => new BsonSerializationProvider(), true);
        public static BsonSerializationProvider Instance => lazy_singleInstance.Value;

        private                 /*Constructor*/ BsonSerializationProvider()
        {
        }
    }
}
