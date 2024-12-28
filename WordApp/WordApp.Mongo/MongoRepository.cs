using MongoDB.Driver;
using System.Globalization;
using System.Linq.Expressions;
using WordApp.Data;

namespace WordApp.Mongo
{
    public class MongoRepository<TEntityType> : IRepository<TEntityType> where TEntityType : IUniqueIdentityEntity
    {

        private readonly IMongoCollection<TEntityType> mongoCollection;
        public                  /*Ctor*/                        MongoRepository(IMongoCollection<TEntityType>? mongoCollection)
        {
            ArgumentNullException.ThrowIfNull(mongoCollection);

            this.mongoCollection = mongoCollection;
        }

        public async Task<IEnumerable<TEntityType>> GetAllAsync() => await mongoCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        public async Task<IEnumerable<TEntityType>> GetAsync(Expression<Func<TEntityType, bool>> filter) => await mongoCollection.Find(filter).ToListAsync().ConfigureAwait(false);
        public async Task<TEntityType?> GetAsync(string id) => await mongoCollection.Find(x => Equals(x.Id, id)).FirstOrDefaultAsync().ConfigureAwait(false);
        public async Task DeleteAsync(TEntityType? item) { if (item is not null) await DeleteAsync(item.Id).ConfigureAwait(false); }
        public async Task DeleteAsync(string id) => await mongoCollection.DeleteOneAsync(x => Equals(x.Id, id)).ConfigureAwait(false);
        public async Task DeleteGuidAsync(string guid) => await mongoCollection.DeleteOneAsync(x => Equals(x.Id, guid)).ConfigureAwait(false);
        public async Task<bool> UpdateAsync(string id, TEntityType? item)
        {
            if (item is not null)
            {
                item.UpdateTime = DateTime.UtcNow;
                ReplaceOneResult result = await mongoCollection.ReplaceOneAsync(x => Equals(x.Id, id), item).ConfigureAwait(false);
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                    return true;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(TEntityType? item) => item is not null && await UpdateAsync(item.Id, item).ConfigureAwait(false);
        public IQueryable<TEntityType> GetAsQueryable(Expression<Func<TEntityType, bool>>? filter = null) { filter ??= _ => true; return mongoCollection.AsQueryable().Where(filter); }
        public async Task<IEnumerable<TEntityType>?> GetAllPaginationAsync(int pageNumber, int pageOffset) =>
            //if (pageNumber < 1 && pageOffset<0)
            //    return null;

            //int skip = (pageNumber - 1) * pageOffset;

            //SortDefinition<EntityType> sort = Builders<EntityType>.Sort.Ascending(x => x.Id);
            //return await mongoCollection.Find(_ => true).Sort(sort).Skip(skip).Limit(pageOffset).ToListAsync();
            await GetAllPaginationAsync(_ => true, pageNumber, pageOffset).ConfigureAwait(false);

        public async Task<IEnumerable<TEntityType>?> GetAllPaginationAsync(Expression<Func<TEntityType, bool>> filter, int pageNumber, int pageOffset)
        {
            if (pageNumber < 1 && pageOffset < 0)
                return null;

            int skip = (pageNumber - 1) * pageOffset;

            SortDefinition<TEntityType> sort = Builders<TEntityType>.Sort.Ascending(x => x.Id);
            return await mongoCollection.Find(filter).Sort(sort).Skip(skip).Limit(pageOffset).ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntityType?> CreateAsync(TEntityType? item)
        {
            if (item is not null)
                await mongoCollection.InsertOneAsync(item).ConfigureAwait(false);

            return item;
        }



        public async Task<IEnumerable<TEntityType>?> CreateManyAsync(IEnumerable<TEntityType> items)
        {

            if (items is not null && items.Any())
                await mongoCollection.InsertManyAsync(items).ConfigureAwait(false);

            return items;
        }

        public async Task<bool> UpdateManyAsync(IEnumerable<TEntityType> items)
        {

            if (items is not null)
            {
                List<WriteModel<TEntityType>> bulkOperations = [];

                foreach (TEntityType entity in items)
                {
                    entity.UpdateTime = DateTime.UtcNow;
                    FilterDefinition<TEntityType> filter = Builders<TEntityType>.Filter.Eq(x => x.Id, entity.Id);
                    ReplaceOneModel<TEntityType> model = new(filter, entity);
                    bulkOperations.Add(model);
                }

                if (bulkOperations.Count > 0)
                {
                    BulkWriteResult<TEntityType> result = await mongoCollection.BulkWriteAsync(bulkOperations).ConfigureAwait(false);
                    if (result.IsAcknowledged && result.ModifiedCount > 0)
                        return true;
                }
            }
            return false;
        }

        public async Task DeleteManyAsync(IEnumerable<TEntityType> items)
        {
            if (items is not null && items.Any())
            {
                FilterDefinition<TEntityType>? filter = Builders<TEntityType>.Filter.In("_id", items.Select(static x => x.ToString()));
                await mongoCollection.DeleteManyAsync(filter).ConfigureAwait(false);
            }
        }

        public async Task DeleteManyAsync(IEnumerable<string> items)
        {
            if (items is not null && items.Any())
            {
                FilterDefinition<TEntityType>? filter = Builders<TEntityType>.Filter.In("_id", items.Select(static x => x.ToString(CultureInfo.CurrentCulture)));
                await mongoCollection.DeleteManyAsync(filter).ConfigureAwait(false);
            }
        }
    }
}
