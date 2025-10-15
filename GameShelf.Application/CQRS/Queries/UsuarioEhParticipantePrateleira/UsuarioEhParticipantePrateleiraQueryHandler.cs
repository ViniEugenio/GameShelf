using GameShelf.Application.ApplicationServices.Interfaces;
using MediatR;

namespace GameShelf.Application.CQRS.Queries.UsuarioEhParticipantePrateleira
{
    public class UsuarioEhParticipantePrateleiraQueryHandler(IPrateleiraService prateleiraService) : IRequestHandler<UsuarioEhParticipantePrateleiraQuery, bool>
    {

        private readonly IPrateleiraService _prateleiraService = prateleiraService;

        public async Task<bool> Handle(UsuarioEhParticipantePrateleiraQuery request, CancellationToken cancellationToken)
        {

            return await _prateleiraService
                .VerificarUsuarioEhParticipantePrateleira(request.PrateleiraId);

        }

    }
}
