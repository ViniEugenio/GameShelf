using GameShelf.JogosConsumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShelf.JogosConsumer.Infrastructure.Persistence.Mappings
{
    public class GeneroMap : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {

            builder
                .HasKey(genero => genero.Id);

            builder
                .Property(genero => genero.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(genero => genero.DataAtivacao)
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder
                .Property(genero => genero.DataDesativacao)
                .HasColumnType("smalldatetime");

            builder
                .Property(genero => genero.DataAlteracao)
                .HasColumnType("smalldatetime");

            builder
                .Property(genero => genero.Ativo)
                .IsRequired()
                .HasColumnType("bit");

        }
    }
}
