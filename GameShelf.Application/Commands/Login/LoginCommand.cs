﻿using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.Commands.Login
{
    public class LoginCommand : IRequest<ResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
