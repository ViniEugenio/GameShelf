using GameShelf.Application.DTOs;
using GameShelf.Application.Validators.ErrorMessages;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameShelf.API.Filters.AuthorizationFilters
{

    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {

        public ClaimAuthorizeAttribute(string claim, EClaimPermissions permissao)
            : base(typeof(ClaimVerifierFilter))
        {
            Arguments = [claim, permissao];
        }

    }

    public class ClaimVerifierFilter : IAuthorizationFilter
    {

        private readonly string _claim;
        private readonly EClaimPermissions _permissao;

        public ClaimVerifierFilter(string claim, EClaimPermissions permissao)
        {
            _claim = claim;
            _permissao = permissao;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            ResponseDTO response = new();

            bool usuarioLogado = context.HttpContext.User.Identity != null
                && context.HttpContext.User.Identity.IsAuthenticated;

            if (!usuarioLogado)
            {

                response
                    .AdicionarErros(UsuarioErros.UsuarioNaoLogado);

                context.Result = new UnauthorizedObjectResult(response);

                return;

            }

            bool usuarioPossuiPermissaoAcesso = context
                .HttpContext
                .User
                .Claims
                .Any(claim =>

                    claim.Type == ClaimsManager.Admin

                    || 

                        claim.Type == _claim
                        && ((EClaimPermissions)Convert.ToInt32(claim.Value)).HasFlag(_permissao)

                    

                );

            if (!usuarioPossuiPermissaoAcesso)
            {

                response
                    .AdicionarErros(UsuarioErros.AcessoNegado);

                context.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };

            }

        }

    }

}
