using GameShelf.JogosConsumer.Domain.Entities;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IJogoRepository : IBaseRepository<Jogo>
    {
        Task<List<string>> FiltrarJogosNaoCadastrados(List<string> jogos);
    }
}
