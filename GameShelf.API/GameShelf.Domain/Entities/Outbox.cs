using GameShelf.Domain.Enums;

namespace GameShelf.Domain.Entities
{
    public class Outbox
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Payload { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public int RetryCount { get; set; }
        public EMessageStatus MessageStatus { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataProximaTentativaPublicacao { get; set; }
        public DateTime? DataPublicacao { get; set; }

    }
}
