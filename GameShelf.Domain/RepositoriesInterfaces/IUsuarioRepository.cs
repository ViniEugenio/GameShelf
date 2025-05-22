using GameShelf.Domain.Entities;
using GameShelf.Domain.Projections;
using GameShelf.Domain.Projections.User;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Security.Claims;

namespace GameShelf.Domain.RepositoriesInterfaces
{
    public interface IUsuarioRepository : IBaseRepository<User>
    {
        Task<IdentityResult> CadastrarUsuario(User user, string password);
        Task<UsuarioSimplificadoProjection> GetUsuarioSimplificado(Guid id);
        Task<PaginatedProjection<UsuarioPaginacaoProjection>> GetUsuarioPaginados(Expression<Func<User, bool>> predicates, int paginaAtual, int quantidade);
        Task<SignInResult> Login(string email, string password);
        Task<LoginProjection> GetInformacoesLoginUsuario(string email);
    }
}
