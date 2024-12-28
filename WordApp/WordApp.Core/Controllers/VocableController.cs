
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordApp.Core.Requests;
using WordApp.Dtos;

namespace WordApp.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VocableController(IMediator mediator) : Controller, IVocableController
    {
        [HttpGet(nameof(GetAllAsync))]
        public async Task<IEnumerable<ReadVocableDto>> GetAllAsync()
        {
            IEnumerable<ReadVocableDto> response = await mediator.Send(new GetAllVocablesRequest());
            return response;
        }
        [HttpGet(nameof(GetAsync))]
        public async Task<ReadVocableDto> GetAsync(string vocableId)
        {
            ReadVocableDto response = await mediator.Send(new GetVocableRequest(vocableId));
            return response;
        }
        [HttpPost(nameof(CreateAsync))]
        public async Task<ReadVocableDto> CreateAsync(CreateVocableDto dto)
        {
            ReadVocableDto response = await mediator.Send(new CreateVocableRequest(dto));
            return response;
        }
        [HttpPut(nameof(UpdateAsync))]
        public async Task<ReadVocableDto> UpdateAsync(UpdateVocableDto dto)
        {
            ReadVocableDto response = await mediator.Send(new UpdateVocableRequest(dto));
            return response;
        }
        [HttpDelete(nameof(DeleteByIdAsync))]
        public async Task<bool> DeleteByIdAsync(string vocableId)
        {
            bool response = await mediator.Send(new DeleteVocableIdRequest(vocableId));
            return response;
        }
        [HttpDelete(nameof(DeleteAsync))]
        public async Task<bool> DeleteAsync(DeleteVocableDto dto)
        {
            bool response = await mediator.Send(new DeleteVocableRequest(dto));
            return response;
        }
    }
}
