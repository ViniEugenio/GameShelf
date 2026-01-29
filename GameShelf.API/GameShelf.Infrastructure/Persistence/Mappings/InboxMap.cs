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
                .Property(inbox => inbox.EventId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(inbox => inbox.DataProcessamentoMensagem)
                .HasColumnType("smalldatetime")
                .IsRequired();

        }
    }
}
