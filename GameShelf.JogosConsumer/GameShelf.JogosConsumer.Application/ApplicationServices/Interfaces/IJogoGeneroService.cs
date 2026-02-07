using GameShelf.JogosConsumer.Domain.Entities;

namespace GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces
{
    public interface IJogoGeneroService
    {
        Task RelacionarGenerosJogos(List<JogoGenero> generosJogos);
    }
}
