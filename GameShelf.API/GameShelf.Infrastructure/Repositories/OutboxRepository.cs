using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Projections.Outbox;
using GameShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Infrastructure.Repositories
{
    public class OutboxRepository(Context context) : BaseRepository<Outbox>(context), IOutboxRepository
    {

        public async Task<List<OutboxPendingMessageProjection>> GetPendingMessages()
        {

            return await _dbSet
                .AsNoTracking()
                .Where(outbox => outbox.MessageStatus == EMessageStatus.Pendente)
                .Select(outbox => new OutboxPendingMessageProjection()
                {
                    Id = outbox.Id,
                    EventId = outbox.EventId,
                    Exchange = outbox.Exchange,
                    Payload = outbox.Payload,
                    RoutingKey = outbox.RoutingKey
                })
                .ToListAsync();

        }

        public async Task MarcarMensagensComoPublicadas(List<Guid> idsMensagens)
        {

            await _dbSet
                .Where(outbox => idsMensagens.Contains(outbox.Id))
                .ExecuteUpdateAsync(outbox =>

                    outbox
                        .SetProperty(outbox => outbox.MessageStatus, EMessageStatus.Publicada)
                        .SetProperty(outbox => outbox.DataPublicacao, DateTime.Now)

                );

        }
    }
}
