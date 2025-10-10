using GameShelf.Domain.Models.Filters.RawG;
using GameShelf.Domain.Models.Projections.RawG;

namespace GameShelf.Domain.Interfaces.ExternalServicesInterfaces
{
    public interface IRawGService
    {
        Task<RawGListGamesResultProjection> GetGames(GetGamesFilter filter);
    }
}
