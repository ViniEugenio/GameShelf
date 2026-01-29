using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameShelf.JogosConsumer.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected readonly Context _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(Context context)
        {

            _context = context;

            _dbSet = _context
                .Set<T>();

        }

        public async Task Add(T model)
        {
            _dbSet.Add(model);
            await SaveChanges();
        }

        public async Task BulkingInsert(List<T> models)
        {

            await _context
                .BulkInsert<T>(models);

        }

        public async Task BulkingUpdate(Expression<Func<T, bool>> condicoes, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> modificacoes)
        {

            await _dbSet
                .Where(condicoes)
                .ExecuteUpdateAsync(modificacoes);

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
