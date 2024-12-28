using System.Linq.Expressions;

namespace WordApp.Data
{
    public interface IEntityBase
    {
        DateTime  CreateTime { get; set; }
        DateTime? UpdateTime { get; set; }
    }
    public interface IUniqueIdentityEntity : IEntityBase
    {
        string Id { get; set; }
    }


    public interface ICreateDto
    {

    }
    public interface IUpdateOrDeleteDto
    {
        string Id { get; set; }
    }
}
