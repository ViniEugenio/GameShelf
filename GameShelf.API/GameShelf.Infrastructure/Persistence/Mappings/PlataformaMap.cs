using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class PlataformaMap : BaseMap<Plataforma>
    {
        public override void Configure(EntityTypeBuilder<Plataforma> builder)
        {

            base.Configure(builder);

            builder
                .Property(plataforma => plataforma.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

        }
    }
}
