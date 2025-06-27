using GameShelf.API.Filters.AuthorizationFilters;
using GameShelf.Application.Commands.CriarPrateleira;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    [Route("/api/prateleira")]
    public class PrateleiraController : BaseController
    {

        public PrateleiraController(IMediator mediator) : base(mediator)
        {
        }

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
