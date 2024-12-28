using System.Reflection;

namespace WordApp.Mongo
{
    public interface IMongoEntityTypeProviderAssemblyLoader
    {
        Assembly? Load();
    }
    public interface IMongoEntityTypeProvider
    {
        public IReadOnlyDictionary<Type, string> GetEntityTypeTypes { get; }
    }
}
