using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Domain.Models.Filters.User;
using GameShelf.Domain.Models.Projections;
using GameShelf.Domain.Models.Projections.User;
using GameShelf.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShelf.Infrastructure.Repositories
{
    public class UsuarioRepository(Context context, UserManager<User> userManager, SignInManager<User> signInManager) : BaseRepository<User>(context), IUsuarioRepository
    {

        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;

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

        public async Task<PaginatedProjection<UsuarioPaginacaoProjection>> GetUsuarioPaginados(GetListagemUsuariosFilter filtro)
        {

            IQueryable<UsuarioPaginacaoProjection> queryListagemUsuarios = _dbSet
                .Where(usuario => (

                        string.IsNullOrEmpty(filtro.Nome)
                        || (usuario.Nome + " " + usuario.Sobrenome).Contains(filtro.Nome)

                    )

                    && (

                        string.IsNullOrEmpty(filtro.Email)
                        || usuario.Email.Contains(filtro.Email)

                    )

                    && (

                        filtro.DataAtivacaoInicio == null
                        || usuario.DataAtivacao >= filtro.DataAtivacaoInicio

                    )
                    && (

                        filtro.DataAtivacaoFim == null
                        || usuario.DataAtivacao <= filtro.DataAtivacaoFim

                    )

                    && usuario.Ativo == filtro.Ativo

                )
                .AsNoTracking()
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

            return await GetPaginated(queryListagemUsuarios, filtro.PaginaAtual, filtro.Quantidade);

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
                    Id = usuario.Id,
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

        public async Task<PaginatedProjection<UsuarioClaimsProjection>> GetUsuariosClaimsPaginados(GetListagemClaimsUsuariosFilter filtro)
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
                        claim
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
                        .Select(join => new ClaimProjection()
                        {
                            Id = join.claim.Id,
                            Type = join.claim.ClaimType,
                            Value = Convert.ToInt32(join.claim.ClaimValue)
                        })
                        .ToList()
                });

            return await GetPaginated(query, filtro.PaginaAtual, filtro.Quantidade);

        }

    }
}
