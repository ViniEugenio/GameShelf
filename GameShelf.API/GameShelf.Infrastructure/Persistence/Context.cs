using EFCore.BulkExtensions;
using GameShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameShelf.Infrastructure.Persistence
{
    public partial class Context(DbContextOptions options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(

                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)

            );

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var dateTimeProps = entityType.ClrType
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime));

                foreach (var prop in dateTimeProps)
                {
                    builder.Entity(entityType.Name)
                        .Property(prop.Name)
                        .HasConversion(dateTimeConverter);
                }
            }

        }

        public async Task BulkInsert<T>(IList<T> entities) where T : class
        {

            await this.BulkInsertAsync(entities, new BulkConfig()
            {
                SetOutputIdentity = true
            });

        }

    }
}
