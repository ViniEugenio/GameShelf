using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class ComentarioMap : BaseMap<Comentario>
    {
        public override void Configure(EntityTypeBuilder<Comentario> builder)
        {

            base.Configure(builder);

            builder
                .HasOne(comentario => comentario.User)
                .WithMany(user => user.ComentariosFeitos)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(comentario => comentario.UserId);

            builder
               .HasOne(comentario => comentario.Analise)
               .WithMany(user => user.ComentariosRecebidos)
               .HasForeignKey(comentario => comentario.AnaliseId);

            builder
                .Property(comentario => comentario.Texto)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

        }
    }
}