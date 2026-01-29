using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.JogosConsumer.Infrastructure.Persistence
{
    public partial class Context(DbContextOptions<Context> options) : DbContext(options)
    {

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
