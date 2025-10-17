using GameShelf.Application.DTOs;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.AtualizarJogos
{
    public class AtualizarJogosCommandHandler : IRequestHandler<AtualizarJogosCommand, ResponseDTO>
    {

        private readonly IMessageBus _messageBus;

        public async Task<ResponseDTO> Handle(AtualizarJogosCommand request, CancellationToken cancellationToken)
        {
            await _messageBus.Publish(null, EQueue.AtualizacaoJogos);
            return new ResponseDTO();
        }

    }
}
