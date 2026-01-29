using GameShelf.JogosConsumer.Domain.Entities;

namespace GameShelf.Domain.Entities
{
    public class Inbox
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime DataProcessamentoMensagem { get; set; }
        public Outbox OutboxEvent { get; set; }
    }
}
