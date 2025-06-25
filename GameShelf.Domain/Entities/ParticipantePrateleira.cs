namespace GameShelf.Domain.Entities
{
    public class ParticipantePrateleira : BaseEntity
    {

        public Guid PrateleiraId { get; set; }
        public Prateleira Prateleira { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
