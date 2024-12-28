using MediatR;
using WordApp.Core.Services;
using WordApp.Dtos;

namespace WordApp.Core.Requests
{
    public class GetAllVocablesRequest() : IRequest<IEnumerable<ReadVocableDto>>
    {
        internal sealed class GetAllVocablesHandler(IVocableService vocableService) : IRequestHandler<GetAllVocablesRequest, IEnumerable<ReadVocableDto>>
        {
            public async Task<IEnumerable<ReadVocableDto>> Handle(GetAllVocablesRequest request, CancellationToken cancellationToken)
            {
                return await vocableService.GetAllAsync().ConfigureAwait(false);
            }
        }
    }
    public class GetVocableRequest(string vocableId) : IRequest<ReadVocableDto>
    {
        private readonly string vocableId = vocableId;
        internal sealed class GetVocableHandler(IVocableService vocableService) : IRequestHandler<GetVocableRequest, ReadVocableDto>
        {
            public async Task<ReadVocableDto> Handle(GetVocableRequest request, CancellationToken cancellationToken)
            {
                return await vocableService.GetAsync(request.vocableId).ConfigureAwait(false);
            }
        }
    }

    public class CreateVocableRequest(CreateVocableDto createVocableDto) : IRequest<ReadVocableDto>
    {
        private readonly CreateVocableDto createVocableDto = createVocableDto;
        internal sealed class CreateVocableHandler(IVocableService vocableService) : IRequestHandler<CreateVocableRequest, ReadVocableDto>
        {
            public async Task<ReadVocableDto> Handle(CreateVocableRequest request, CancellationToken cancellationToken)
            {
                return await vocableService.CreateAsync(request.createVocableDto).ConfigureAwait(false);
            }
        }
    }
    public class UpdateVocableRequest(UpdateVocableDto updateVocableDto) : IRequest<ReadVocableDto>
    {
        private readonly UpdateVocableDto updateVocableDto = updateVocableDto;
        internal sealed class UpdateVocableHandler(IVocableService vocableService) : IRequestHandler<UpdateVocableRequest, ReadVocableDto>
        {
            public async Task<ReadVocableDto> Handle(UpdateVocableRequest request, CancellationToken cancellationToken)
            {
                return await vocableService.UpdateAsync(request.updateVocableDto).ConfigureAwait(false);
            }
        }
    }

    public class DeleteVocableRequest(DeleteVocableDto deleteVocableDto) : IRequest<bool>
    {
        private readonly DeleteVocableDto deleteVocableDto = deleteVocableDto;
        internal sealed class DeleteVocableHandler(IVocableService vocableService) : IRequestHandler<DeleteVocableRequest, bool>
        {
            public async Task<bool> Handle(DeleteVocableRequest request, CancellationToken cancellationToken)
            {
                return await vocableService.DeleteAsync(request.deleteVocableDto).ConfigureAwait(false);
            }
        }
    }
    public class DeleteVocableIdRequest(string vocableId) : IRequest<bool>
    {
        private readonly string vocableId = vocableId;
        internal sealed class DeleteVocableHandler(IVocableService vocableService) : IRequestHandler<DeleteVocableIdRequest, bool>
        {
            public async Task<bool> Handle(DeleteVocableIdRequest request, CancellationToken cancellationToken)
            {
                return await vocableService.DeleteAsync(request.vocableId).ConfigureAwait(false);
            }
        }
    }
}
