using GameShelf.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public class InboxRepository(Context context) : BaseRepository<Inbox>(context), IInboxRepository
    {
        public async Task<bool> VerificarEventoJaFoiProcessado(Guid eventId)
        {
            return await _dbSet.AnyAsync(inbox => inbox.EventId == eventId);
        }

        public async Task MarcarEventoComoProcessado(Guid eventId)
        {
            
        }

    }
}
