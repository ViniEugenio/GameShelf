using MediatR;

namespace GameShelf.Application.CQRS.Queries.UsuarioEhParticipantePrateleira
{
    public class UsuarioEhParticipantePrateleiraQuery : IRequest<bool>
    {
        public Guid PrateleiraId { get; set; }
    }
}
