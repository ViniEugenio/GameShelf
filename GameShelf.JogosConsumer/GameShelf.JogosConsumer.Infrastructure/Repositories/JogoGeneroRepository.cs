using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Infrastructure.Persistence;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class JogoGeneroRepository(Context context) : BaseRepository<JogoGenero>(context), IJogoGeneroRepository
    {
    }
}
