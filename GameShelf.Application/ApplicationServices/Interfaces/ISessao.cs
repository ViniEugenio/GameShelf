using GameShelf.Application.DTOs.UsuarioDTO;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface ISessao
    {
        void SetUsuarioLogado(UsuarioLoginDTO usuarioLogado);
        UsuarioLoginDTO GetUsuarioLogado();
    }
}
