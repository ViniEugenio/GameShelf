using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class JogoMap : BaseMap<Jogo>
    {
        public override void Configure(EntityTypeBuilder<Jogo> builder)
        {

            base.Configure(builder);

            builder
                .Property(jogo => jogo.Nome)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder
                .Property(jogo => jogo.Descricao)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder
                .Property(jogo => jogo.Imagem)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

        }
    }
}