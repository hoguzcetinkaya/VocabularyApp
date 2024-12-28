namespace WordApp.Data
{
    public interface IDbContext
    {
        IRepository<TEntityType>? GetRepository<TEntityType>() where TEntityType : IUniqueIdentityEntity;
    }
}
