using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IPlataformaRepository : IBaseRepository<Plataforma>
    {
        Task<List<string>> FiltrarPlataformasNaoCadastradas(List<RawGPlatformDetailsProjection> plataformas);
    }
}
