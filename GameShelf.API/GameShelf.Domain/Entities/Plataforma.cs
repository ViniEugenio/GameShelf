namespace GameShelf.Domain.Entities
{
    public class Plataforma : BaseEntity
    {

        public string Nome { get; set; }

        public List<JogoPlataforma> Jogos { get; set; } = [];

    }
}
