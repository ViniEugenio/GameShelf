using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.AlterarUsuario
{
    public class AlterarUsuarioCommandHandler : IRequestHandler<AlterarUsuarioCommand, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService;

        public AlterarUsuarioCommandHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseDTO> Handle(AlterarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioService.AlterarUsuario(request);
        }

    }
}
