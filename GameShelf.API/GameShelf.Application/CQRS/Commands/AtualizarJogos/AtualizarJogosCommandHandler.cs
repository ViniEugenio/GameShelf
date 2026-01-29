using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.AtualizarJogos
{
    public class AtualizarJogosCommandHandler(IOutboxService outboxService) : IRequestHandler<AtualizarJogosCommand, ResponseDTO>
    {

        private readonly IOutboxService _outboxService = outboxService;

        public async Task<ResponseDTO> Handle(AtualizarJogosCommand request, CancellationToken cancellationToken)
        {
            return await _outboxService.AddOutboxRequisitionAtualizarJogos();
        }

    }
}
