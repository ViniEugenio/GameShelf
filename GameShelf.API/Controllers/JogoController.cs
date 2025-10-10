using GameShelf.Application.CQRS.Commands.AtualizarJogos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{
    [Route("/api/jogo")]
    public class JogoController : BaseController
    {

        public JogoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarJogos()
        {
            return await Respond(new AtualizarJogosCommand());
        }

    }
}
