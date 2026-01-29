namespace GameShelf.Domain.Entities
{
    public class Genero : BaseEntity
    {

        public string Nome { get; set; }

        public List<JogoGenero> Jogos { get; set; } = [];

    }
}
