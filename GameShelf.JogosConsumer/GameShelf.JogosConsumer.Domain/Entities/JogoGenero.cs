namespace GameShelf.JogosConsumer.Domain.Entities
{
    public class JogoGenero : BaseEntity
    {

        public Guid JogoId { get; set; }
        public Jogo Jogo { get; set; }

        public Guid GeneroId { get; set; }
        public Genero Genero { get; set; }

    }
}
