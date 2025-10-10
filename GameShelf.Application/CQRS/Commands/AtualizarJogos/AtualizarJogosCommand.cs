using GameShelf.Application.DTOs;
using MediatR;

namespace GameShelf.Application.CQRS.Commands.AtualizarJogos
{
    public class AtualizarJogosCommand : IRequest<ResponseDTO>
    {
    }
}
