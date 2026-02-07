using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Infrastructure.Persistence;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class JogoPlataformaRepository(Context context) : BaseRepository<JogoPlataforma>(context), IJogoPlataformaRepository
    {
    }
}
