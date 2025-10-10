using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.CriarPrateleira
{
    public class CriarPrateleiraCommand : IRequest<ResponseDTO>
    {
        public string Nome { get; set; }
        public List<Guid> Participantes { get; set; } = [];
    }
}
