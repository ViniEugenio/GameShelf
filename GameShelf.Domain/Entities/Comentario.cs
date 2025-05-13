namespace GameShelf.Domain.Entities
{
    public class Comentario : BaseEntity
    {

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid AnaliseId { get; set; }
        public Analise Analise { get; set; }

        public string Texto { get; set; }

    }
}
