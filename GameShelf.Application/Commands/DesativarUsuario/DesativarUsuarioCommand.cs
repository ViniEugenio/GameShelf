using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.Commands.DesativarUsuario
{
    public class DesativarUsuarioCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
