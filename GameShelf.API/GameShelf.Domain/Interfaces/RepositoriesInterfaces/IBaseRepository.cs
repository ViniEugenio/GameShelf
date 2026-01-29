using GameShelf.Domain.Models.Projections;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameShelf.Domain.Interfaces.RepositoriesInterfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Add(T model);
        Task BulkingInsert(List<T> models);
        Task Update(T model);
        Task BulkingUpdate(Expression<Func<T, bool>> condicoes, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> modificacoes);
        Task Save();
        Task<bool> Exists(Expression<Func<T, bool>> condicoes);
        Task<T> GetById(Guid id);
        Task<PaginatedProjection<T>> GetPaginated<T>(IQueryable<T> query, int paginaAtual, int take);
        Task<int> Count(Expression<Func<T, bool>> condicoes);
    }
}
