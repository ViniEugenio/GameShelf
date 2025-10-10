using GameShelf.Domain.Models.Projections.RawG;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IPlataformaService
    {
        Task AtualizarPlataformas(List<RawGPlataformaProjection> plataformas);
    }
}
