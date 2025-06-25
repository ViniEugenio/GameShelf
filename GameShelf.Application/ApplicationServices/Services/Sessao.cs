using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs.UsuarioDTO;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class Sessao : ISessao
    {

        private UsuarioLoginDTO _usuarioLogado;

        public void SetUsuarioLogado(UsuarioLoginDTO usuarioLogado)
        {
            _usuarioLogado = usuarioLogado;
        }

        public UsuarioLoginDTO GetUsuarioLogado()
        {
            return _usuarioLogado;
        }

    }
}
