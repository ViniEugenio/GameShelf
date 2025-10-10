using GameShelf.Domain.Entities;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Infrastructure.Persistence;

namespace GameShelf.Infrastructure.Repositories
{
    public class PlataformaRepository(Context context) : BaseRepository<Plataforma>(context), IPlataformaRepository
    {
    }
}
