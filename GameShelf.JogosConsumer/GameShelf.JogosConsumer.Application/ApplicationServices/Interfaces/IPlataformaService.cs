using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces
{
    public interface IPlataformaService
    {
        Task<List<string>> FiltrarPlataformasAindaNaoCadastradas(List<RawGPlatformDetailsProjection> plataformas);
        Task CadastrarNovasPlataformas(List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas);
    }
}
