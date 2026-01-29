namespace GameShelf.Domain.Models.Projections.Outbox
{
    public class OutboxPendingMessageProjection
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Payload { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
    }
}
