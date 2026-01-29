using MediatR;

namespace GameShelf.JogosConsumer.Application.CQRS.Commands.MarcarEventoProcessado
{
    public class MarcarEventoProcessadoCommand(Guid eventId) : IRequest<Unit>
    {
        public Guid EventId { get; set; } = eventId;
    }
}
