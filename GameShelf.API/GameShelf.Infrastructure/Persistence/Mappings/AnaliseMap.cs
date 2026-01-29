using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
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
                .HasOne(analise => analise.JogoPrateleira)
                .WithMany(jogoPrateleira => jogoPrateleira.AnalisesRecebidas)
                .HasForeignKey(analise => analise.JogoPrateleiraId)
                .OnDelete(DeleteBehavior.Restrict);

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