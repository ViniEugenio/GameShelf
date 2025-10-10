using GameShelf.Domain.Entities;

namespace GameShelf.Domain.Interfaces.RepositoriesInterfaces
{
    public interface IPrateleiraRepository : IBaseRepository<Prateleira>
    {
        Task<bool> VerificarUsuarioEhParticipantePrateleira(Guid prateleiraId, Guid usuarioLogadoId);
    }
}
