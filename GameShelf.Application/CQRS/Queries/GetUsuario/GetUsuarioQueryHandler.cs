using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Queries.GetUsuario
{
    public class GetUsuarioQueryHandler : IRequestHandler<GetUsuarioQuery, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService;

        public GetUsuarioQueryHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseDTO> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioService.GetUsuarioSimplificado(request);
        }
    }
}
