using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.Queries.GetListagemUsuarios
{
    public class GetListagemUsuariosQueryHandler : IRequestHandler<GetListagemUsuariosQuery, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService;

        public GetListagemUsuariosQueryHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseDTO> Handle(GetListagemUsuariosQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioService.GetUsuariosPaginados(request);
        }
    }
}
