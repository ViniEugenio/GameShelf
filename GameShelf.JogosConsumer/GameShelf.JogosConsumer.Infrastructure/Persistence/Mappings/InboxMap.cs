using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class InboxMap : IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {

            builder
                .HasKey(inbox => inbox.Id);

            builder
                .HasOne(inbox => inbox.OutboxEvent)
                .WithOne(outbox => outbox.InboxEvent)
                .HasForeignKey<Inbox>(inbox => inbox.EventId);

            builder
                .Property(inbox => inbox.DataProcessamentoMensagem)
                .IsRequired();

        }
    }
}
