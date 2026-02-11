using GameShelf.JogosConsumer.Domain.Projections.RawG;
using Refit;

namespace GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices
{
    public interface IRawGService
    {

        [Get("/games")]
        Task<ApiResponse<RawGListGamesResultProjection>> GetGames([Query] RawGGetGamesFilterProjection query);

    }
}
