using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class JogoPrateleiraMap : BaseMap<JogoPrateleira>
    {
        public override void Configure(EntityTypeBuilder<JogoPrateleira> builder)
        {

            base.Configure(builder);

            builder
                .HasOne(jogoPrateleira => jogoPrateleira.Prateleira)
                .WithMany(prateleira => prateleira.Jogos)
                .HasForeignKey(jogoPrateleira => jogoPrateleira.PrateleiraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(jogoPrateleira => jogoPrateleira.Jogo)
                .WithMany(jogo => jogo.Prateleiras)
                .HasForeignKey(jogoPrateleira => jogoPrateleira.JogoId);

            builder
                .HasOne(jogoPrateleira => jogoPrateleira.User)
                .WithMany(user => user.JogosInseridosEmPrateleiras)
                .HasForeignKey(jogoPrateleira => jogoPrateleira.UserId);

        }
    }
}