using GameShelf.Domain.Entities;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Infrastructure.Persistence;

namespace GameShelf.Infrastructure.Repositories
{
    public class JogoRepository(Context context) : BaseRepository<Jogo>(context), IJogoRepository
    {
    }
}
