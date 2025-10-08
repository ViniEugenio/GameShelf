namespace GameShelf.Domain.Entities
{
    public class Prateleira : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Nome { get; set; }

        public List<ParticipantePrateleira> Participantes { get; set; } = [];
        public List<JogoPrateleira> Jogos { get; set; } = [];

    }
}
