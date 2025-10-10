using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Projections;
using GameShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameShelf.Infrastructure.Repositories
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

            await _dbSet
                .AddAsync(model);

            await Save();

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

        public async Task Update(T model)
        {

            _dbSet
                .Update(model);

            await Save();

        }

        public async Task Save()
        {

            await _context
                .SaveChangesAsync();

        }

        public async Task<bool> Exists(Expression<Func<T, bool>> condicoes)
        {

            return await _dbSet
                .AnyAsync(condicoes);

        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<PaginatedProjection<T>> GetPaginated<T>(IQueryable<T> query, int paginaAtual, int take)
        {

            int quantidadeTotal = await query
                .CountAsync();

            if (quantidadeTotal == 0)
            {
                return new PaginatedProjection<T>(quantidadeTotal, []);
            }

            int skip = (paginaAtual - 1) * take;

            var paginacao = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new PaginatedProjection<T>(quantidadeTotal, paginacao);

        }

        public async Task<int> Count(Expression<Func<T, bool>> condicoes)
        {

            return await _dbSet
                .CountAsync(condicoes);

        }

    }
}
