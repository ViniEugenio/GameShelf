namespace GameShelf.Domain.Entities
{
    public class JogoPrateleira : BaseEntity
    {

        public Guid PrateleiraId { get; set; }
        public Prateleira Prateleira { get; set; }

        public Guid JogoId { get; set; }
        public Jogo Jogo { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<Analise> AnalisesRecebidas { get; set; } = [];

    }
}
