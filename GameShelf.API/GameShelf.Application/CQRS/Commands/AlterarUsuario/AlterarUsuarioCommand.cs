using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.AlterarUsuario
{
    public class AlterarUsuarioCommand : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
    }
}
