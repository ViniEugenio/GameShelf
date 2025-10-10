using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Queries.GetUsuario
{
    public class GetUsuarioQuery : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
