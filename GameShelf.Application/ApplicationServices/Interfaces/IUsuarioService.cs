using GameShelf.Application.Commands.AlterarUsuario;
using GameShelf.Application.Commands.CadastrarUsuario;
using GameShelf.Application.Commands.DesativarUsuario;
using GameShelf.Application.Commands.Login;
using GameShelf.Application.DTOs;
using GameShelf.Application.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Application.Queries.GetUsuario;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResponseDTO> CadastrarUsuario(CadastrarUsuarioCommand command);
        Task<ResponseDTO> AlterarUsuario(AlterarUsuarioCommand command);
        Task<ResponseDTO> GetUsuarioSimplificado(GetUsuarioQuery query);
        Task<ResponseDTO> DesativarUsuario(DesativarUsuarioCommand command);
        Task<ResponseDTO> GetUsuariosPaginados(GetListagemUsuariosQuery query);
        Task<ResponseDTO> Login(LoginCommand command);
        Task<ResponseDTO> GetListagemClaimsUsuarios(GetListagemClaimsUsuariosQuery query);
    }
}
