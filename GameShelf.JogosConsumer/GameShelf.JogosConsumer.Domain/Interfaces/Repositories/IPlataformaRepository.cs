using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Projections.Plataforma;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IPlataformaRepository : IBaseRepository<Plataforma>
    {
        Task<List<string>> FiltrarPlataformasNaoCadastradas(List<string> plataformas);
        Task<List<PlataformaJaCadastradaProjection>> GetPlataformasFiltradasPorNome(List<string> plataformas);
    }
}
