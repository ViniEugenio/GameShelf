using GameShelf.Application.CQRS.Commands.AlterarUsuario;
using GameShelf.Domain.Entities;

namespace GameShelf.Application.Extensions.EntitiesExtensions
{
    public static class UsuarioExtensions
    {

        public static void AtualizarUsuario(this User usuarioParaAlteracao, AlterarUsuarioCommand command)
        {
            usuarioParaAlteracao.Nome = command.Nome;
            usuarioParaAlteracao.Sobrenome = command.Sobrenome;
            usuarioParaAlteracao.Email = command.Email;
            usuarioParaAlteracao.DataAlteracao = DateTime.Now;
        }

        public static void DesativarUsuario(this User usuarioParaDesativacao)
        {
            usuarioParaDesativacao.DataDesativacao = DateTime.Now;
            usuarioParaDesativacao.DataAlteracao = DateTime.Now;
            usuarioParaDesativacao.Ativo = false;
        }

    }
}
