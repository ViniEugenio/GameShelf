using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder
                .Property(user => user.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder
                .Property(user => user.Sobrenome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder
                .Property(user => user.DataAtivacao)
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder
                .Property(user => user.DataDesativacao)
                .HasColumnType("smalldatetime");

            builder
                .Property(entity => entity.DataAlteracao)
                .HasColumnType("smalldatetime");

            builder
                .Property(user => user.Ativo)
                .IsRequired()
                .HasColumnType("bit");

        }
    }
}
