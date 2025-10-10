using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class ParticipantePrateleiraMap : BaseMap<ParticipantePrateleira>
    {
        public override void Configure(EntityTypeBuilder<ParticipantePrateleira> builder)
        {

            base.Configure(builder);

            builder
               .HasOne(participante => participante.User)
               .WithMany(user => user.PrateleirasParticipante)
               .HasForeignKey(participante => participante.UserId);

            builder
                .HasOne(participante => participante.Prateleira)
                .WithMany(prateleira => prateleira.Participantes)
                .HasForeignKey(participante => participante.PrateleiraId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
