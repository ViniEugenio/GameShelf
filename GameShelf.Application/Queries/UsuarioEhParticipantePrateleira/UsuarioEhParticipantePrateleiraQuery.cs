using MediatR;

namespace GameShelf.Application.Queries.UsuarioEhParticipantePrateleira
{
    public class UsuarioEhParticipantePrateleiraQuery : IRequest<bool>
    {
        public Guid PrateleiraId { get; set; }
    }
}
