using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Queries.GetListagemUsuarios
{
    public class GetListagemUsuariosQueryHandler(IUsuarioService usuarioService) : IRequestHandler<GetListagemUsuariosQuery, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService = usuarioService;

        public async Task<ResponseDTO> Handle(GetListagemUsuariosQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioService.GetUsuariosPaginados(request);
        }
    }
}
