using WordApp.Dtos;
using WordApp.Entities;

namespace WordApp.Core.Converter
{
    public static class DtoConverter
    {
        public static ReadVocableDto Convert(Vocable entity) 
        {
            return new ReadVocableDto
            {
                Id                  = entity.Id,
                CreateTime          = entity.CreateTime,
                UpdateTime          = entity.UpdateTime,
                Word                = entity.Word,
                Meanings            = entity.Meanings,
                Pronunciation       = entity.Pronunciation,
                WordCountability    = entity.WordCountability,
                WordLevel           = entity.WordLevel,
                WordType            = entity.WordType,
            };
        }
    }
}
