using WordApp.Dtos;

namespace WordApp.Core.Controllers
{
    public interface IVocableController
    {
        Task<IEnumerable<ReadVocableDto>>       GetAllAsync         ();
        Task<ReadVocableDto>                    GetAsync            (string vocableId);
        Task<ReadVocableDto>                    CreateAsync         (CreateVocableDto dto);
        Task<ReadVocableDto>                    UpdateAsync         (UpdateVocableDto dto);
        Task<bool>                              DeleteByIdAsync     (string vocableId);
        Task<bool>                              DeleteAsync         (DeleteVocableDto dto);
    }
}
