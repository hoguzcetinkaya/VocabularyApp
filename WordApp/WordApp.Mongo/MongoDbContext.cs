using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WordApp.Data;

namespace WordApp.Mongo
{
    public class MongoDbContext : IDbContext
    {
        private readonly IReadOnlyDictionary<Type, string> typeCollectionTable;
        private readonly IMongoDatabase? mongoDatabase;

        public                  /*Ctor*/                            MongoDbContext(IConfiguration configuration,IMongoEntityTypeProvider typeProvider)
        {
            ArgumentNullException.ThrowIfNull(typeProvider);

            MongoClient mongoClient = new MongoClient("mongodb://localhost:29999/");
            mongoDatabase = mongoClient.GetDatabase("WordApp");
            typeCollectionTable = typeProvider.GetEntityTypeTypes;
        }
        public IRepository<TEntityType>? GetRepository<TEntityType>() where TEntityType : IUniqueIdentityEntity => typeCollectionTable.TryGetValue(typeof(TEntityType), out string? collectionName)
                                                                                                                              ? new MongoRepository<TEntityType>(mongoDatabase?.GetCollection<TEntityType>(collectionName))
                                                                                                                              : null;
    }
}
