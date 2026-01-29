using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class OutboxMap : IEntityTypeConfiguration<Outbox>
    {
        public void Configure(EntityTypeBuilder<Outbox> builder)
        {

            builder
                .HasKey(outbox => outbox.Id);

            builder
                .Property(outbox => outbox.EventId)
                .HasColumnType("varchar(36)")
                .IsRequired();

            builder
                .Property(outbox => outbox.Payload)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder
                .Property(outbox => outbox.Exchange)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder
                .Property(outbox => outbox.RoutingKey)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder
                .Property(outbox => outbox.RetryCount)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(outbox => outbox.MessageStatus)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(outbox => outbox.DataCriacao)
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder
                .Property(outbox => outbox.DataProximaTentativaPublicacao)
                .HasColumnType("smalldatetime");

            builder
                .Property(outbox => outbox.DataPublicacao)
                .HasColumnType("smalldatetime");

        }
    }
}
