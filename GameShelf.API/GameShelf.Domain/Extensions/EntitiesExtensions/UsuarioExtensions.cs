using GameShelf.Domain.Entities;

namespace GameShelf.Application.Extensions.EntitiesExtensions
{
    public static class UsuarioExtensions
    {

        public static void DesativarUsuario(this User usuarioParaDesativacao)
        {
            usuarioParaDesativacao.DataDesativacao = DateTime.Now;
            usuarioParaDesativacao.DataAlteracao = DateTime.Now;
            usuarioParaDesativacao.Ativo = false;
        }

    }
}
