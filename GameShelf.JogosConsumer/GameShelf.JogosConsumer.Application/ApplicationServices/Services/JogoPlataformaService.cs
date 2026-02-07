using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Domain.Entities;
using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Services
{
    public class JogoPlataformaService(IJogoPlataformaRepository jogoPlataformaRepository) : IJogoPlataformaService
    {

        public readonly IJogoPlataformaRepository _jogoPlataformaRepository = jogoPlataformaRepository;

        public async Task RelacionarPlataformasJogos(List<JogoPlataforma> plataformasJogos)
        {
            await _jogoPlataformaRepository.BulkingInsert(plataformasJogos);
        }
    }
}
