using GameShelf.JogosConsumer.Application.DTOs.Jogos;
using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces
{
    public interface IGeneroService
    {
        Task<List<string>> FiltrarGenerosAindaNaoCadastrados(List<RawGGenreProjection> generos);
        Task CadastrarNovosGeneros(List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas);
        Task<List<Guid>> GetIdsGenerosFiltradosPorNome(List<InfosAindaNaoCadastradasDTO> infosAindaNaoCadastradas);
    }
}
