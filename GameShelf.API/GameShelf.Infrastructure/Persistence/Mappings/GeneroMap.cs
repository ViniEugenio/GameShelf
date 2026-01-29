using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class GeneroMap : BaseMap<Genero>
    {
        public override void Configure(EntityTypeBuilder<Genero> builder)
        {

            base.Configure(builder);

            builder
                .Property(genero => genero.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

        }
    }
}
