using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Projections.RawG;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class JogoService(IJogoRepository jogoRepository) : IJogoService
    {

        private readonly IJogoRepository _jogoRepository = jogoRepository;

        public async Task AtualizarJogos(List<RawGListGamesResultProjection> jogos)
        {
            throw new NotImplementedException();
        }

    }
}
