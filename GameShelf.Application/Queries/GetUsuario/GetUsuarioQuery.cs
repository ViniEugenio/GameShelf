using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.Queries.GetUsuario
{
    public class GetUsuarioQuery : IRequest<ResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
