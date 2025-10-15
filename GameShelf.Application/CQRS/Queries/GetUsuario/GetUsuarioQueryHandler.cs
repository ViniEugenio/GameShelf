using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Queries.GetUsuario
{
    public class GetUsuarioQueryHandler(IUsuarioService usuarioService) : IRequestHandler<GetUsuarioQuery, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService = usuarioService;

        public async Task<ResponseDTO> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioService.GetUsuarioSimplificado(request);
        }
    }
}
