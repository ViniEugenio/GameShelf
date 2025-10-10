using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Models.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameShelf.API.Filters.AuthorizationFilters
{

    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {

        public ClaimAuthorizeAttribute(string claim, EClaimPermissions permissao)
            : base(typeof(ClaimAuthorizationFilter))
        {
            Arguments = [claim, permissao];
        }

    }

    public class ClaimAuthorizationFilter : IAuthorizationFilter
    {

        private readonly string _claim;
        private readonly EClaimPermissions _permissao;

        public ClaimAuthorizationFilter(string claim, EClaimPermissions permissao)
        {
            _claim = claim;
            _permissao = permissao;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            ResponseDTO response = new();

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
