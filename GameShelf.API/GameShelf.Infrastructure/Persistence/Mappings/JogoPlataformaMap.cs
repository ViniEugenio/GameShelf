using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class JogoPlataformaMap : BaseMap<JogoPlataforma>
    {
        public override void Configure(EntityTypeBuilder<JogoPlataforma> builder)
        {

            base.Configure(builder);

            builder
                .HasOne(jogoPlataforma => jogoPlataforma.Jogo)
                .WithMany(jogo => jogo.Plataformas)
                .HasForeignKey(jogoPlataforma => jogoPlataforma.JogoId);

            builder
                .HasOne(jogoPlataforma => jogoPlataforma.Plataforma)
                .WithMany(plataforma => plataforma.Jogos)
                .HasForeignKey(jogoPlataforma => jogoPlataforma.PlataformaId);

        }
    }
}