using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace GameShelf.Domain.RepositoriesInterfaces
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
        Task<PaginatedResult> GetPaginated<Projecao, PaginatedResult>(IQueryable<Projecao> query, int paginaAtual, int take);
    }
}
