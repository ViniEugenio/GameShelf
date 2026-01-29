using GameShelf.Application.CQRS.Queries.UsuarioEhParticipantePrateleira;
using GameShelf.Application.CQRS.Validators.ErrorMessages;
using GameShelf.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameShelf.API.Filters.AuthorizationFilters
{

    public class PrateleiraAuthorizationAttribute : TypeFilterAttribute
    {

        public PrateleiraAuthorizationAttribute()
            : base(typeof(PrateleiraAuthorizationFilter))
        {
        }

    }

    public class PrateleiraAuthorizationFilter : IAsyncAuthorizationFilter
    {

        private readonly IMediator _mediator;

        public PrateleiraAuthorizationFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        private async Task<ResponseDTO> ValidarPermissaoUsuarioPrateleira(AuthorizationFilterContext context)
        {

            ResponseDTO response = new();

            string prateleiraIdValue = context
                 .HttpContext
                 .Request
                 .Query["prateleiraId"];

            var prateleiraIdObj = context
                .RouteData
                .Values
                .GetValueOrDefault("prateleiraId");

            bool inputIdPrateleiraValido = !string.IsNullOrEmpty(prateleiraIdValue)
                || prateleiraIdObj != null;

            if (!inputIdPrateleiraValido)
            {

                response.AdicionarErros(PrateleiraErros.SemPermissaoPrateleira);
                return response;

            }

            Guid prateleiraId = Guid.Empty;

            bool inputIdPrateleiraConvertido = Guid.TryParse(prateleiraIdValue, out prateleiraId)
                || Guid.TryParse(prateleiraIdObj.ToString(), out prateleiraId);

            if (!inputIdPrateleiraConvertido)
            {

                response.AdicionarErros(PrateleiraErros.SemPermissaoPrateleira);
                return response;

            }

            UsuarioEhParticipantePrateleiraQuery query = new()
            {
                PrateleiraId = prateleiraId
            };

            bool usuarioEhParticipantePrateleira = await _mediator
                .Send(query);

            if (!usuarioEhParticipantePrateleira)
            {
                response.AdicionarErros(PrateleiraErros.SemPermissaoPrateleira);
            }

            return response;

        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            ResponseDTO response = await ValidarPermissaoUsuarioPrateleira(context);


            if (!response.IsValid())
            {

                context.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };

            }

        }
    }

}
