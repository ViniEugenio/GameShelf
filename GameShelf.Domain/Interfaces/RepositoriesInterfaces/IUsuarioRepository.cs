using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Models.Filters.User;
using GameShelf.Domain.Models.Projections;
using GameShelf.Domain.Models.Projections.User;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Domain.Interfaces.RepositoriesInterfaces
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
