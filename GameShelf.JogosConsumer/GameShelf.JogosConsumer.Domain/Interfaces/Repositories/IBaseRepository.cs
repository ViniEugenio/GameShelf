using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameShelf.JogosConsumer.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task BulkingInsert(List<T> models);
        Task BulkingUpdate(Expression<Func<T, bool>> condicoes, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> modificacoes);
        Task Add(T model);
        Task SaveChanges();
    }
}
