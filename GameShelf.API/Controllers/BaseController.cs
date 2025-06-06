﻿using GameShelf.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.API.Controllers
{

    [ApiController]
    public abstract class BaseController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Respond(IRequest<ResponseDTO> request)
        {

            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid())
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);

        }

    }

}
