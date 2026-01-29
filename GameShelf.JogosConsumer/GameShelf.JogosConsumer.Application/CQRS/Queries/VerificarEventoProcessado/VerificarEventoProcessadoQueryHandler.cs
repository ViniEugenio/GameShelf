using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using MediatR;

namespace GameShelf.JogosConsumer.Application.CQRS.Queries.VerificarEventoProcessado
{
    public class VerificarEventoProcessadoQueryHandler(IInboxRepository inboxRepository) : IRequestHandler<VerificarEventoProcessadoQuery, bool>
    {

        private readonly IInboxRepository _inboxRepository = inboxRepository;

        public async Task<bool> Handle(VerificarEventoProcessadoQuery request, CancellationToken cancellationToken)
        {
            return await _inboxRepository.VerificarEventoJaFoiProcessado(request.EventId);
        }

    }
}
