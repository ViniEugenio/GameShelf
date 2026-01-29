using GameShelf.API.Filters.AuthorizationFilters;
using GameShelf.Application.CQRS.Commands.CriarPrateleira;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    [Route("/api/prateleira")]
    public class PrateleiraController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost]
        [UsuarioLogadoAuthorize]
        public async Task<IActionResult> CriarPrateleira(CriarPrateleiraCommand command)
        {
            return await Respond(command);
        }

        [HttpGet("{prateleiraId}")]
        [UsuarioLogadoAuthorize]
        [PrateleiraAuthorization]
        public async Task<IActionResult> GetPrateleira(Guid prateleiraId)
        {
            return Ok("usuário pode visualizar a prateleira!");
        }

    }
}
