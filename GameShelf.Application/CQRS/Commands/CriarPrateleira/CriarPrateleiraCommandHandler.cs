using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.CriarPrateleira
{
    public class CriarPrateleiraCommandHandler(IPrateleiraService prateleiraService) : IRequestHandler<CriarPrateleiraCommand, ResponseDTO>
    {

        private readonly IPrateleiraService _prateleiraService = prateleiraService;

        public async Task<ResponseDTO> Handle(CriarPrateleiraCommand request, CancellationToken cancellationToken)
        {
            return await _prateleiraService.CriarPrateleira(request);
        }
    }
}
