using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.Login
{
    public class LoginCommandHandler(IUsuarioService usuarioService) : IRequestHandler<LoginCommand, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService = usuarioService;

        public async Task<ResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioService.Login(request);
        }

    }
}
