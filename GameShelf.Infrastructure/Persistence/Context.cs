using EFCore.BulkExtensions;
using GameShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Infrastructure.Persistence
{
    public partial class Context : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
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
