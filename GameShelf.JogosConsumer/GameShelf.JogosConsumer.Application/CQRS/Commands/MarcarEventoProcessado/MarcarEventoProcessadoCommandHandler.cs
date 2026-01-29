using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using MediatR;

namespace GameShelf.JogosConsumer.Application.CQRS.Commands.MarcarEventoProcessado
{
    public class MarcarEventoProcessadoCommandHandler(IInboxRepository inboxRepository) : IRequestHandler<MarcarEventoProcessadoCommand, Unit>
    {

        private readonly IInboxRepository _inboxRepository = inboxRepository;

        public async Task<Unit> Handle(MarcarEventoProcessadoCommand request, CancellationToken cancellationToken)
        {

            await _inboxRepository.Add(new()
            {
                EventId = request.EventId,
                DataProcessamentoMensagem = DateTime.Now
            });

            return Unit.Value;

        }
    }
}
