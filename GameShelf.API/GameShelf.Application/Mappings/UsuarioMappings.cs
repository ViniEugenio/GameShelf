using GameShelf.Application.CQRS.Commands.AlterarUsuario;
using GameShelf.Application.CQRS.Commands.CadastrarUsuario;
using GameShelf.Application.CQRS.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.CQRS.Queries.GetListagemUsuarios;
using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Models.Filters.User;
using GameShelf.Domain.Models.Projections;
using GameShelf.Domain.Models.Projections.User;

namespace GameShelf.Application.Mappings
{
    public static class UsuarioMappings
    {

        public static User MapNovoUsuario(CadastrarUsuarioCommand command)
        {

            return new User()
            {
                Nome = command.Nome,
                Sobrenome = command.Sobrenome,
                Email = command.Email,
                UserName = command.Email
            };

        }

        public static UsuarioSimplificadoDTO MapUsuarioSimplificado(UsuarioSimplificadoProjection projection)
        {

            return new UsuarioSimplificadoDTO()
            {
                Id = projection.Id,
                Nome = projection.Nome,
                Sobrenome = projection.Sobrenome,
                Email = projection.Email,
                Ativo = projection.Ativo
            };

        }

        public static GetListagemUsuariosFilter MapFiltroListagemUsuarios(GetListagemUsuariosQuery query)
        {

            return new GetListagemUsuariosFilter()
            {
                Nome = query.Nome,
                Email = query.Email,
                Ativo = query.Ativo,
                DataAtivacaoInicio = query.DataAtivacaoInicio,
                DataAtivacaoFim = query.DataAtivacaoFim,
                PaginaAtual = query.PaginaAtual,
                Quantidade = query.Quantidade
            };

        }

        public static PaginatedResultDTO<UsuarioListagemDTO> MapListagemsUsuarios(PaginatedProjection<UsuarioPaginacaoProjection> projection, GetListagemUsuariosQuery query)
        {

            return new PaginatedResultDTO<UsuarioListagemDTO>()
            {
                PaginaAtual = query.PaginaAtual,
                QuantidadePorPagina = query.Quantidade,
                QuantidadeTotal = projection.QuantidadeTotal,
                Listagem = [..

                projection
                    .Listagem
                    .Select(usuario => new UsuarioListagemDTO()
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        Ativo = usuario.Ativo,
                        DataAtivacao = usuario.DataAtivacao,
                        DataDesativacao = usuario.DataDesativacao,
                        DataAlteracao = usuario.DataAlteracao
                    })

                ]
            };

        }

        public static LoginDTO MapLoginUsuario(LoginProjection projection, string jwt)
        {

            return new LoginDTO()
            {
                JWT = jwt,
                Usuario = new UsuarioLoginDTO()
                {
                    Id = projection.Usuario.Id,
                    Nome = projection.Usuario.Nome,
                    Email = projection.Usuario.Email
                }
            };

        }

        public static GetListagemClaimsUsuariosFilter MapFiltroListagemClaimsUsuarios(GetListagemClaimsUsuariosQuery query)
        {

            return new GetListagemClaimsUsuariosFilter()
            {
                Nome = query.Nome,
                Email = query.Email,
                DataAtivacaoInicio = query.DataAtivacaoInicio,
                DataAtivacaoFim = query.DataAtivacaoFim,
                PaginaAtual = query.PaginaAtual,
                Quantidade = query.Quantidade,
                ClaimsTypes = query.ClaimTypes
            };

        }

        public static List<UsuarioClaimsDTO> MapListagemClaimsUsuarios(PaginatedProjection<UsuarioClaimsProjection> projection)
        {

            return [..

                projection
                    .Listagem
                    .Select(usuario => new UsuarioClaimsDTO()
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        Permissoes = [..

                            usuario
                            .Claims
                            .Select(claim => new PermissoesDTO(claim))

                        ]
                    })

            ];

        }

        public static void AtualizarUsuario(this User usuarioParaAlteracao, AlterarUsuarioCommand command)
        {
            usuarioParaAlteracao.Nome = command.Nome;
            usuarioParaAlteracao.Sobrenome = command.Sobrenome;
            usuarioParaAlteracao.Email = command.Email;
            usuarioParaAlteracao.DataAlteracao = DateTime.Now;
        }

    }
}