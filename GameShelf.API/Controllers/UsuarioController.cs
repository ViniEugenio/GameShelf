using GameShelf.API.Filters.AuthorizationFilters;
using GameShelf.Application.CQRS.Commands.AlterarUsuario;
using GameShelf.Application.CQRS.Commands.CadastrarUsuario;
using GameShelf.Application.CQRS.Commands.DesativarUsuario;
using GameShelf.Application.CQRS.Commands.Login;
using GameShelf.Application.CQRS.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.CQRS.Queries.GetListagemUsuarios;
using GameShelf.Application.CQRS.Queries.GetUsuario;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Models.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{

    [Route("/api/user")]
    public class UsuarioController(IMediator mediator) : BaseController(mediator)
    {
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
        [UsuarioLogadoAuthorize]
        public async Task<IActionResult> AlterarUsuario([FromBody] AlterarUsuarioCommand command)
        {
            return await Respond(command);
        }

        [HttpGet]
        [UsuarioLogadoAuthorize]
        public async Task<IActionResult> GetUsuario([FromQuery] GetUsuarioQuery query)
        {
            return await Respond(query);
        }

        [HttpPatch("DesativarUsuario")]
        [UsuarioLogadoAuthorize]
        [ClaimAuthorize(ClaimsManager.UserAdmin, EClaimPermissions.Delete)]
        public async Task<IActionResult> DesativarUsuario([FromBody] DesativarUsuarioCommand command)
        {
            return await Respond(command);
        }

        [HttpGet("GetListagemUsuarios")]
        [UsuarioLogadoAuthorize]
        [ClaimAuthorize(ClaimsManager.UserAdmin, EClaimPermissions.Read)]
        public async Task<IActionResult> GetListagemUsuarios([FromQuery] GetListagemUsuariosQuery query)
        {
            return await Respond(query);
        }

        [HttpGet("GetListagemClaimsUsuarios")]
        [UsuarioLogadoAuthorize]
        [ClaimAuthorize(ClaimsManager.UserAdmin, EClaimPermissions.Read)]
        public async Task<IActionResult> GetListagemClaimsUsuarios([FromQuery] GetListagemClaimsUsuariosQuery query)
        {
            return await Respond(query);
        }

    }
}
