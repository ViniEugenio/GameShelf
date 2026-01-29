using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.Infrastructure.Persistence.Mappings
{
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {

            builder
                .HasKey(entity => entity.Id);

            builder
                .Property(entity => entity.DataAtivacao)
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder
                .Property(entity => entity.DataDesativacao)
                .HasColumnType("smalldatetime");

            builder
                .Property(entity => entity.DataAlteracao)
                .HasColumnType("smalldatetime");

            builder
                .Property(entity => entity.Ativo)
                .IsRequired()
                .HasColumnType("bit");

        }
    }
}
