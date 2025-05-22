using GameShelf.Domain.Entities;
using GameShelf.Domain.Projections;
using GameShelf.Domain.Projections.User;
using GameShelf.Domain.RepositoriesInterfaces;
using GameShelf.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace GameShelf.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<User>, IUsuarioRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsuarioRepository(Context context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CadastrarUsuario(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<UsuarioSimplificadoProjection> GetUsuarioSimplificado(Guid id)
        {

            return await _dbSet
                .Where(usuario => usuario.Id == id)
                .Select(usuario => new UsuarioSimplificadoProjection()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    Ativo = usuario.Ativo
                })
                .SingleAsync();

        }

        public async Task<PaginatedProjection<UsuarioPaginacaoProjection>> GetUsuarioPaginados(Expression<Func<User, bool>> predicates, int paginaAtual, int quantidade)
        {

            var queryListagemUsuarios = _dbSet
                .Where(predicates)
                .Select(usuario => new UsuarioPaginacaoProjection()
                {
                    Id = usuario.Id,
                    Nome = $"{usuario.Nome} {usuario.Sobrenome}",
                    Email = usuario.Email,
                    Ativo = usuario.Ativo,
                    DataAtivacao = usuario.DataAtivacao,
                    DataAlteracao = usuario.DataAlteracao,
                    DataDesativacao = usuario.DataDesativacao
                });

            return await GetPaginated(queryListagemUsuarios, paginaAtual, quantidade);

        }

        public async Task<SignInResult> Login(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }

        public async Task<LoginProjection> GetInformacoesLoginUsuario(string email)
        {

            User usuario = await _userManager
                .FindByEmailAsync(email);

            List<Claim> claims = [.. await _userManager.GetClaimsAsync(usuario)];
            claims.Add(new Claim(ClaimTypes.Name, email));

            return new LoginProjection()
            {
                Claims = new(claims),
                Usuario = new()
                {
                    Nome = $"{usuario.Nome} {usuario.Sobrenome}",
                    Email = usuario.Email
                }
            };

        }

    }
}
