namespace WordApp.Mongo
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class MongoEntityAttribute(string collectionName) : Attribute
    {
        public string CollectionName { get; } = collectionName;
    }
}
