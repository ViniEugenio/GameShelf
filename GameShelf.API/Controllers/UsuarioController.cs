using GameShelf.API.Filters;
using GameShelf.Application.Commands.AlterarUsuario;
using GameShelf.Application.Commands.CadastrarUsuario;
using GameShelf.Application.Commands.DesativarUsuario;
using GameShelf.Application.Commands.Login;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Application.Queries.GetUsuario;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Security;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return await Respond(command);
        }

        [HttpPut]
        [ClaimAuthorize(ClaimsManager.User, EClaimPermissions.Update)]
        public async Task<IActionResult> AlterarUsuario([FromBody] AlterarUsuarioCommand command)
        {
            return await Respond(command);
        }

        [HttpGet]
        [ClaimAuthorize(ClaimsManager.User, EClaimPermissions.Read)]
        public async Task<IActionResult> GetUsuario([FromQuery] GetUsuarioQuery query)
        {
            return await Respond(query);
        }

        [HttpPatch("DesativarUsuario")]
        [ClaimAuthorize(ClaimsManager.Admin, EClaimPermissions.Delete)]
        public async Task<IActionResult> DesativarUsuario([FromBody] DesativarUsuarioCommand command)
        {
            return await Respond(command);
        }

        [HttpGet("GetListagemUsuarios")]
        [ClaimAuthorize(ClaimsManager.Admin, EClaimPermissions.Read)]
        public async Task<IActionResult> GetListagemUsuarios([FromQuery] GetListagemUsuariosQuery query)
        {
            return await Respond(query);
        }

    }
}
