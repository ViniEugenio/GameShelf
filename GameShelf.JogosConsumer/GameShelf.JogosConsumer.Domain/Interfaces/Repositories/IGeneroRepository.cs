using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Projections.Genero;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IGeneroRepository : IBaseRepository<Genero>
    {
        Task<List<string>> FiltrarGenerosNaoCadastrados(List<string> generos);
        Task<List<GeneroJaCadastradoProjection>> GetGenerosFiltradosPorNome(List<string> nomesGeneros);
    }
}
