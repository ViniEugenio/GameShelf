using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
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

            IQueryable<UsuarioPaginacaoProjection> queryListagemUsuarios = _dbSet
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

        public async Task AdicionarClaims(User user, Dictionary<string, EClaimPermissions> claims)
        {

            List<Claim> claimsFormatadas = [..claims
                .Select(claim => new Claim(claim.Key, Convert.ToInt32(claim.Value).ToString()))];

            await _userManager.AddClaimsAsync(user, claimsFormatadas);

        }

        public async Task<PaginatedProjection<UsuarioClaimsProjection>> GetUsuariosClaimsPaginados(UsuarioClaimFilterProjection filtro)
        {

            IQueryable<UsuarioClaimsProjection> query = _dbSet
                .Join(

                    _context
                        .UserClaims
                        .Where(claim =>

                            filtro.ClaimsTypes.Count == 0
                            || filtro.ClaimsTypes.Contains(claim.ClaimType)

                        )
                        .AsNoTracking(),

                    usuario => usuario.Id,
                    claim => claim.UserId,
                    (usuario, claim) => new
                    {
                        usuario,
                        Claim = claim.ToClaim()
                    }

                )
                .GroupBy(join => new
                {
                    join.usuario.Id,
                    Nome = join.usuario.Nome + " " + join.usuario.Sobrenome,
                    join.usuario.Email,
                    join.usuario.DataAtivacao,
                    join.usuario.Ativo
                })
                .Where(agrupamento =>

                    (

                        string.IsNullOrEmpty(filtro.Nome)
                        || agrupamento.Key.Nome.Contains(filtro.Nome)

                    )

                    && (

                        string.IsNullOrEmpty(filtro.Email)
                        || agrupamento.Key.Email.Contains(filtro.Email)

                    )

                    && (

                        filtro.DataAtivacaoInicio == null
                        || agrupamento.Key.DataAtivacao >= filtro.DataAtivacaoInicio

                    )

                    && (

                        filtro.DataAtivacaoFim == null
                        || agrupamento.Key.DataAtivacao <= filtro.DataAtivacaoFim

                    )

                    && agrupamento.Key.Ativo

                )
                .Select(agrupamento => new UsuarioClaimsProjection()
                {
                    Id = agrupamento.Key.Id,
                    Nome = agrupamento.Key.Nome,
                    Email = agrupamento.Key.Email,
                    Claims = agrupamento
                        .Select(join => join.Claim)
                        .ToList()
                });

            return await GetPaginated(query, filtro.PaginaAtual, filtro.Quantidade);

        }

    }
}
