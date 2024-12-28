using WordApp.Core.Converter;
using WordApp.Data;
using WordApp.Dtos;
using WordApp.Entities;

namespace WordApp.Core.Services
{
    public class VocableService(IDbContext dbContext) : IVocableService
    {
        public async Task<IEnumerable<ReadVocableDto>> GetAllAsync()
        {
            IEnumerable<Vocable> s = await dbContext.GetRepository<Vocable>()!.GetAllAsync();
            if (s.Any())
            {
                return s.Select(v => new ReadVocableDto
                {
                    Id = v.Id,
                    Meanings = v.Meanings,
                    Pronunciation = v.Pronunciation,
                    WordType = v.WordType,
                    WordLevel = v.WordLevel,
                    WordCountability = v.WordCountability
                });
            }
            return [];
        }
        public async Task<ReadVocableDto>   GetAsync(string id)
        {

            Vocable? v = await dbContext.GetRepository<Vocable>()!.GetAsync(id);
            return v is not null ? DtoConverter.Convert(v) : throw new NotImplementedException();

        }
        public async Task<ReadVocableDto>   CreateAsync(CreateVocableDto dto)
        {
            DtoNullCheck(dto);
            Vocable? vocable = WordExistsCheck(dto.Word);
            if (vocable is not null)
                throw new ArgumentException("Word Already exists");
            Vocable v = new Vocable
            {
                Word                = dto.Word,
                Meanings            = dto.Meanings,
                Pronunciation       = dto.Pronunciation,
                WordType            = dto.WordType,
                WordLevel           = dto.WordLevel,
                WordCountability    = dto.WordCountability,
                IsActive            = dto.IsActive,
                WordHalleri         = dto.WordHalleri,
            }.SetCreateParams();


            Vocable? y = await dbContext.GetRepository<Vocable>()!.CreateAsync(v);
            if(y is null)
                throw new NotImplementedException();
            return DtoConverter.Convert(y);
           
        }
        public async Task<ReadVocableDto>   UpdateAsync(UpdateVocableDto dto)
        {
            DtoNullCheck(dto);
            Vocable? vocable = WordExistsCheck(dto.Id);
            if(vocable is null)
                throw new NotImplementedException();
            vocable.WordLevel           = dto.WordLevel;
            vocable.WordType            = dto.WordType;
            vocable.Word                = dto.Word;
            vocable.Pronunciation       = dto.Pronunciation;
            vocable.Meanings            = dto.Meanings;
            vocable.WordCountability    = dto.WordCountability;
            vocable.WordHalleri         = dto.WordHalleri;
            bool updated = await dbContext.GetRepository<Vocable>()!.UpdateAsync(vocable);

            return updated ? DtoConverter.Convert(vocable) : throw new ArgumentException($"Updating {dto.Id}");
        }
        public async Task<bool>             DeleteAsync(DeleteVocableDto dto)
        {
            DtoNullCheck(dto);
            Vocable? vocable = WordExistsCheck(dto.Id);
            if (vocable is null)
                return false;
            await dbContext.GetRepository<Vocable>()!.DeleteAsync(vocable);
            return true;
        }
        public async Task<bool>             DeleteAsync(string id)
        {
            await dbContext.GetRepository<Vocable>()!.DeleteAsync(id);
            return true;
        }
        private void                        DtoNullCheck<T>(T dto) where T : class
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
        }
        private Vocable?                    WordExistsCheck(string key) => dbContext.GetRepository<Vocable>()!.GetAsync(x => x.Word == key || x.Id == key).Result.FirstOrDefault();

    }
}
