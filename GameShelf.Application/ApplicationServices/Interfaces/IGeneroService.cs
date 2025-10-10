using GameShelf.Domain.Models.Projections.RawG;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IGeneroService
    {
        Task AtualizarGeneros(List<RawGGeneroProjection> generos);
    }
}
