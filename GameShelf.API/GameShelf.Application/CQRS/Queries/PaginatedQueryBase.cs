using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Queries
{
    public abstract class PaginatedQueryBase : IRequest<ResponseDTO>
    {

        public int Quantidade { get; set; } = 10;
        public int PaginaAtual { get; set; } = 1;

    }
}
