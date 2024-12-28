using WordApp.Data;

namespace WordApp.Dtos
{
    public class DeleteVocableDto : IUpdateOrDeleteDto
    {
        public string Id { get; set; } = string.Empty;
    }
}
