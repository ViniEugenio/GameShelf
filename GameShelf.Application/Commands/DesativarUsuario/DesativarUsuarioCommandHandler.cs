using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.Commands.DesativarUsuario
{
    public class DesativarUsuarioCommandHandler : IRequestHandler<DesativarUsuarioCommand, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService;

        public DesativarUsuarioCommandHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseDTO> Handle(DesativarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioService.DesativarUsuario(request);
        }

    }
}
