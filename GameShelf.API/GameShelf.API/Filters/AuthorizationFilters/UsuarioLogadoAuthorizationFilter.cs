using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameShelf.API.Filters.AuthorizationFilters
{

    public class UsuarioLogadoAuthorizeAttribute : TypeFilterAttribute
    {

        public UsuarioLogadoAuthorizeAttribute()
            : base(typeof(UsuarioLogadoAuthorizationFilter))
        {
        }

    }

    public class UsuarioLogadoAuthorizationFilter : IAuthorizationFilter
    {

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

        }

    }
}
