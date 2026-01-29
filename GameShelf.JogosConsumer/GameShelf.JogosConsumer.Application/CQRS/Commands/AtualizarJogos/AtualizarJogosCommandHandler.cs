using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using MediatR;

namespace GameShelf.JogosConsumer.Application.CQRS.Commands.AtualizarJogos
{
    public class AtualizarJogosCommandHandler(IJogoService jogoService) : IRequestHandler<AtualizarJogosCommand, Unit>
    {

        private readonly IJogoService _jogoService = jogoService;

        public async Task<Unit> Handle(AtualizarJogosCommand request, CancellationToken cancellationToken)
        {
            await _jogoService.AtualizarJogos();
            return Unit.Value;
        }
    }
}
