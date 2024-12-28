using System.Linq.Expressions;

namespace WordApp.Data
{
    public interface IRepository<TEntityType> where TEntityType : IUniqueIdentityEntity
    {
        Task<IEnumerable<TEntityType>>          GetAllAsync                 ();
        Task<TEntityType?>                      GetAsync                    (string id);
        Task<IEnumerable<TEntityType>>          GetAsync                    (Expression<Func<TEntityType, bool>> filter);
        Task<TEntityType?>                      CreateAsync                 (TEntityType item);
        Task<IEnumerable<TEntityType>?>         CreateManyAsync             (IEnumerable<TEntityType> items);
        Task<bool>                              UpdateAsync                 (TEntityType item);
        Task<bool>                              UpdateAsync                 (string id, TEntityType item);
        Task<bool>                              UpdateManyAsync             (IEnumerable<TEntityType> items);
        Task                                    DeleteAsync                 (TEntityType item);
        Task                                    DeleteAsync                 (string id);
        Task                                    DeleteGuidAsync             (string guid);
        Task                                    DeleteManyAsync             (IEnumerable<TEntityType> items);
        Task                                    DeleteManyAsync             (IEnumerable<string> items);
        Task<IEnumerable<TEntityType>?>         GetAllPaginationAsync       (int pageNumber, int pageOffset);
        Task<IEnumerable<TEntityType>?>         GetAllPaginationAsync       (Expression<Func<TEntityType, bool>> filter, int pageNumber, int pageOffset);


    }
}
