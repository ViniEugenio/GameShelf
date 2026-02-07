using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class JogoGeneroService(IJogoGeneroRepository jogoGeneroRepository) : IJogoGeneroService
    {

        private readonly IJogoGeneroRepository _jogoGeneroRepository = jogoGeneroRepository;

        public async Task RelacionarGenerosJogos(List<JogoGenero> generosJogos)
        {
            await _jogoGeneroRepository.BulkingInsert(generosJogos);
        }
    }
}
