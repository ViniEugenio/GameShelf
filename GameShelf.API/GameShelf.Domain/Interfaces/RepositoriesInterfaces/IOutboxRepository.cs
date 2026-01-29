using GameShelf.Domain.Entities;
using GameShelf.Domain.Models.Projections.Outbox;

namespace GameShelf.Domain.Interfaces.RepositoriesInterfaces
{
    public interface IOutboxRepository : IBaseRepository<Outbox>
    {
        Task<List<OutboxPendingMessageProjection>> GetPendingMessages();
        Task MarcarMensagensComoPublicadas(List<Guid> idsMensagens);
    }
}
