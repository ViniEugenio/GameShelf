using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Projections.RawG;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IJogoRepository : IBaseRepository<Jogo>
    {
        Task<List<string>> FiltrarJogosNaoCadastrados(List<RawGGameProjection> jogos);
    }
}
