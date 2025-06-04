using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Filters.User;
using GameShelf.Domain.Projections;
using GameShelf.Domain.Projections.User;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Domain.RepositoriesInterfaces
{
    public interface IUsuarioRepository : IBaseRepository<User>
    {
        Task<IdentityResult> CadastrarUsuario(User user, string password);
        Task<UsuarioSimplificadoProjection> GetUsuarioSimplificado(Guid id);
        Task<PaginatedProjection<UsuarioPaginacaoProjection>> GetUsuarioPaginados(GetListagemUsuariosFilter filtro);
        Task<SignInResult> Login(string email, string password);
        Task<LoginProjection> GetInformacoesLoginUsuario(string email);
        Task AdicionarClaims(User user, Dictionary<string, EClaimPermissions> claims);
        Task<PaginatedProjection<UsuarioClaimsProjection>> GetUsuariosClaimsPaginados(GetListagemClaimsUsuariosFilter filtro);
    }
}
