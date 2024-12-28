using WordApp.Dtos;

namespace WordApp.Core.Services
{
    public interface IVocableService
    {
        Task<IEnumerable<ReadVocableDto>>   GetAllAsync     ();
        Task<ReadVocableDto>                GetAsync        (string id);
        Task<ReadVocableDto>                CreateAsync     (CreateVocableDto dto);
        Task<ReadVocableDto>                UpdateAsync     (UpdateVocableDto dto);
        Task<bool>                          DeleteAsync     (DeleteVocableDto dto);
        Task<bool>                          DeleteAsync     (string id);
    }
}
