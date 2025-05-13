using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Domain.Entities;
using GameShelf.Domain.RepositoriesInterfaces;
using GameShelf.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<User>, IUsuarioRepository
    {

        private readonly UserManager<User> _userManager;

        public UsuarioRepository(Context context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CadastrarUsuario(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<T> GetUsuarioSimplificado<T>(Guid id)
        {

            return await _dbSet
                .Where(usuario => usuario.Id == id)
                .Select(usuario => (T)(object)new UsuarioSimplificadoDTO()
                {
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    Ativo = usuario.Ativo
                })
                .SingleAsync();

        }

        public async Task<Response> GetUsuarioPaginados<Response, Filtro>(Filtro filtro)
        {

            GetListagemUsuariosQuery query = filtro as GetListagemUsuariosQuery;

            var queryListagemUsuarios = _dbSet
                .Where(usuario =>

                    (

                        string.IsNullOrEmpty(query.Nome)
                        || $"{usuario.Nome} {usuario.Sobrenome}".Contains(query.Nome)

                    )
                    && (

                        string.IsNullOrEmpty(query.Email)
                        || usuario.Email.Contains(query.Email)

                    )
                    && (

                        query.DataAtivacaoInicio == null
                        || usuario.DataAtivacao >= query.DataAtivacaoInicio

                    )
                    && (

                        query.DataAtivacaoFim == null
                        || usuario.DataAtivacao <= query.DataAtivacaoFim

                    )

                    && usuario.Ativo == query.Ativo

                )
                .Select(usuario => new UsuarioListagemDTO()
                {
                    Nome = $"{usuario.Nome} {usuario.Sobrenome}",
                    Email = usuario.Email,
                    Ativo = usuario.Ativo,
                    DataAtivacao = usuario.DataAtivacao,
                    DataAlteracao = usuario.DataAlteracao,
                    DataDesativacao = usuario.DataDesativacao
                });

            return (Response)(object)await GetPaginated<UsuarioListagemDTO, PaginatedResultDTO<UsuarioListagemDTO>>(queryListagemUsuarios, query.PaginaAtual, query.Skip, query.Quantidade);

        }

    }
}
