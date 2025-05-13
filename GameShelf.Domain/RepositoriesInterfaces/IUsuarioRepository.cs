using GameShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Domain.RepositoriesInterfaces
{
    public interface IUsuarioRepository : IBaseRepository<User>
    {
        Task<IdentityResult> CadastrarUsuario(User user, string password);
        Task<T> GetUsuarioSimplificado<T>(Guid id);
        Task<Response> GetUsuarioPaginados<Response, Filtro>(Filtro filtro);
    }
}
