using GameShelf.Domain.Models.Projections.RawG;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IJogoService
    {
        Task AtualizarJogos(List<RawGListGamesResultProjection> jogos);
    }
}
