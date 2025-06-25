using GameShelf.Application.Commands.CriarPrateleira;
using GameShelf.Application.DTOs;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IPrateleiraService
    {
        Task<ResponseDTO> CriarPrateleira(CriarPrateleiraCommand command);
    }
}
