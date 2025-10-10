using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Queries.GetListagemClaimsUsuarios
{
    public class GetListagemClaimsUsuariosQueryHandler : IRequestHandler<GetListagemClaimsUsuariosQuery, ResponseDTO>
    {

        private readonly IUsuarioService _usuarioService;

        public GetListagemClaimsUsuariosQueryHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<ResponseDTO> Handle(GetListagemClaimsUsuariosQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioService.GetListagemClaimsUsuarios(request);
        }
    }
}
