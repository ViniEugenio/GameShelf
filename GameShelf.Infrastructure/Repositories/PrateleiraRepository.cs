using GameShelf.Domain.Entities;
using GameShelf.Domain.RepositoriesInterfaces;
using GameShelf.Infrastructure.Persistence;

namespace GameShelf.Infrastructure.Repositories
{
    public class PrateleiraRepository : BaseRepository<Prateleira>, IPrateleiraRepository
    {
        public PrateleiraRepository(Context context) : base(context)
        {
        }
    }
}
