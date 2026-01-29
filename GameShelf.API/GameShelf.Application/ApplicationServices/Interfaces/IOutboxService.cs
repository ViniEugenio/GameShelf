using GameShelf.Application.DTOs;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IOutboxService
    {
        Task<ResponseDTO> AddOutboxRequisitionAtualizarJogos();
        Task PublicarMensagensPendentes();
    }
}
