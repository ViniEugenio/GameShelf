using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.DesativarUsuario
{
    public class DesativarUsuarioCommandHandler(IUsuarioService usuarioService) : IRequestHandler<DesativarUsuarioCommand, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService = usuarioService;

        public async Task<ResponseDTO> Handle(DesativarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioService.DesativarUsuario(request);
        }

    }
}
