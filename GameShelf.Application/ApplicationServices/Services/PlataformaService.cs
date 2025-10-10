using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Projections.RawG;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class PlataformaService(IPlataformaRepository plataformaRepository) : IPlataformaService
    {

        private readonly IPlataformaRepository _plataformaRepository = plataformaRepository;

        public async Task AtualizarPlataformas(List<RawGPlataformaProjection> plataformas)
        {
            throw new NotImplementedException();
        }
    }
}
