using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class JogoGeneroMap : BaseMap<JogoGenero>
    {
        public override void Configure(EntityTypeBuilder<JogoGenero> builder)
        {

            base.Configure(builder);

            builder
                .HasOne(jogoGenero => jogoGenero.Jogo)
                .WithMany(jogo => jogo.Generos)
                .HasForeignKey(jogoGenero => jogoGenero.JogoId);

            builder
                .HasOne(jogoGenero => jogoGenero.Genero)
                .WithMany(genero => genero.Jogos)
                .HasForeignKey(jogoGenero => jogoGenero.GeneroId);

        }
    }
}