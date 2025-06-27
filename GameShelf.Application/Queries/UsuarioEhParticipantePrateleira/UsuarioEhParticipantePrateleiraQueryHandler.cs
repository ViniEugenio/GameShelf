using GameShelf.Application.ApplicationServices.Interfaces;
using MediatR;

namespace GameShelf.Application.Queries.UsuarioEhParticipantePrateleira
{
    public class UsuarioEhParticipantePrateleiraQueryHandler : IRequestHandler<UsuarioEhParticipantePrateleiraQuery, bool>
    {

        private readonly IPrateleiraService _prateleiraService;

        public UsuarioEhParticipantePrateleiraQueryHandler(IPrateleiraService prateleiraService)
        {
            _prateleiraService = prateleiraService;
        }

        public async Task<bool> Handle(UsuarioEhParticipantePrateleiraQuery request, CancellationToken cancellationToken)
        {

            return await _prateleiraService
                .VerificarUsuarioEhParticipantePrateleira(request.PrateleiraId);

        }

    }
}
