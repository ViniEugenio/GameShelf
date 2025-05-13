using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Mappings
{
    public class AnaliseMap : BaseMap<Analise>
    {
        public override void Configure(EntityTypeBuilder<Analise> builder)
        {

            base.Configure(builder);

            builder
                .HasOne(analise => analise.User)
                .WithMany(user => user.AnalisesFeitas)
                .HasForeignKey(analise => analise.UserId);

            builder
                .HasOne(analise => analise.Jogo)
                .WithMany(user => user.AnalisesRecebidas)
                .HasForeignKey(analise => analise.JogoId);

            builder
                .Property(analise => analise.Texto)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder
                .Property(analise => analise.Classificacao)
                .IsRequired()
                .HasColumnType("decimal(2, 1)");

        }
    }
}