using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class PrateleiraMap : BaseMap<Prateleira>
    {
        public override void Configure(EntityTypeBuilder<Prateleira> builder)
        {

            base.Configure(builder);

            builder
                .HasOne(prateleira => prateleira.User)
                .WithMany(usuario => usuario.MinhasPrateleiras)
                .HasForeignKey(prateleira => prateleira.UserId);

            builder
                .Property(prateleira => prateleira.Nome)
                .IsRequired()
                .HasColumnType("varchar(500)");

        }
    }
}