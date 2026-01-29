using MediatR;

namespace GameShelf.JogosConsumer.Application.CQRS.Queries.VerificarEventoProcessado
{
    public class VerificarEventoProcessadoQuery(Guid eventId) : IRequest<bool>
    {
        public Guid EventId { get; set; } = eventId;
    }
}
