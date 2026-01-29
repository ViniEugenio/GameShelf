using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IGeneroRepository : IBaseRepository<Genero>
    {
        Task<List<string>> FiltrarGenerosNaoCadastrados(List<RawGGenreProjection> generos);
        Task<List<Guid>> GetIdsGenerosFiltradosPorNome(List<string> nomesGeneros);
    }
}
