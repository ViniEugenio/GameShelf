using GameShelf.Application.Commands.AlterarUsuario;
using GameShelf.Application.Commands.CadastrarUsuario;
using GameShelf.Application.Commands.DesativarUsuario;
using GameShelf.Application.Commands.Login;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Application.Queries.GetUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{

    [Route("/api/user")]
    public class UsuarioController : BaseController
    {

        public UsuarioController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioCommand command)
        {
            return await Respond(command);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarUsuario([FromBody] AlterarUsuarioCommand command)
        {
            return await Respond(command);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuario([FromQuery] GetUsuarioQuery query)
        {
            return await Respond(query);
        }

        [HttpPatch("DesativarUsuario")]
        public async Task<IActionResult> DesativarUsuario([FromBody] DesativarUsuarioCommand command)
        {
            return await Respond(command);
        }

        [HttpGet("GetListagemUsuarios")]
        public async Task<IActionResult> GetListagemUsuarios([FromQuery] GetListagemUsuariosQuery query)
        {
            return await Respond(query);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return await Respond(command);
        }

    }
}
