using GameShelf.Domain.Entities;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IInboxRepository : IBaseRepository<Inbox>
    {
        Task<bool> VerificarEventoJaFoiProcessado(Guid eventId);
    }
}
