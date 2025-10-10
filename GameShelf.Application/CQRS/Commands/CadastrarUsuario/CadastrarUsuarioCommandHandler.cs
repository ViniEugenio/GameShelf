using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService;

        public CadastrarUsuarioCommandHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseDTO> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {

            return await _usuarioService
                .CadastrarUsuario(request);

        }
    }
}
