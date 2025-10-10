using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Projections.RawG;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class GeneroService(IGeneroRepository generoRepository) : IGeneroService
    {

        private readonly IGeneroRepository _generoRepository = generoRepository;

        public async Task AtualizarGeneros(List<RawGGeneroProjection> generos)
        {
            throw new NotImplementedException();
        }
    }
}
