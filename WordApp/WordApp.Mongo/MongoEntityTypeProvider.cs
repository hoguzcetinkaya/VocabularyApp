using System.Collections.ObjectModel;
using System.Reflection;

namespace WordApp.Mongo
{
    public class MongoEntityTypeProvider : IMongoEntityTypeProvider
    {
        private readonly IReadOnlyDictionary<Type, string> typeCollectionTable;
        public                  /*Ctor*/                            MongoEntityTypeProvider(IMongoEntityTypeProviderAssemblyLoader assemblyLoader)
        {
            ArgumentNullException.ThrowIfNull(assemblyLoader);

            Assembly? assembly = assemblyLoader.Load();
            Dictionary<Type, string> table = [];

            if (assembly is not null)
                foreach (Type item in assembly.GetTypes())
                    if (item.GetCustomAttribute<MongoEntityAttribute>() is MongoEntityAttribute attr)
                        table[item] = attr.CollectionName;

            typeCollectionTable = new ReadOnlyDictionary<Type, string>(table);
        }
        public IReadOnlyDictionary<Type, string> GetEntityTypeTypes => typeCollectionTable!;
    }
}
