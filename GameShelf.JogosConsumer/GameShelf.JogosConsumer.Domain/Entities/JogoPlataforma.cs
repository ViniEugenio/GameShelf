namespace GameShelf.JogosConsumer.Domain.Entities
{
    public class JogoPlataforma : BaseEntity
    {

        public Guid JogoId { get; set; }
        public Jogo Jogo { get; set; }

        public Guid PlataformaId { get; set; }
        public Plataforma Plataforma { get; set; }

    }
}
