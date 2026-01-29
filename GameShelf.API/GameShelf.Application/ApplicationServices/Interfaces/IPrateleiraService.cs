using GameShelf.Application.CQRS.Commands.CriarPrateleira;
using GameShelf.Application.DTOs;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IPrateleiraService
    {
        Task<ResponseDTO> CriarPrateleira(CriarPrateleiraCommand command);
        Task<bool> VerificarUsuarioEhParticipantePrateleira(Guid prateleiraId);
    }
}
