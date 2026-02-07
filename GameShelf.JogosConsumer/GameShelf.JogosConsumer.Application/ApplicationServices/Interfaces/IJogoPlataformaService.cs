using GameShelf.JogosConsumer.Domain.Entities;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces
{
    public interface IJogoPlataformaService
    {
        Task RelacionarPlataformasJogos(List<JogoPlataforma> plataformasJogos);
    }
}
