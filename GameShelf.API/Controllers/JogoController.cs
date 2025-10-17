using GameShelf.Application.CQRS.Commands.AtualizarJogos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    [Route("/api/jogo")]
    public class JogoController(IMediator mediator) : BaseController(mediator)
    {

        [HttpPost("AtualizarJogos")]
        public async Task<IActionResult> AtualizarJogos()
        {
            return await Respond(new AtualizarJogosCommand());
        }

    }
}
