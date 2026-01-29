using GameShelf.Application.CQRS.Commands.AlterarUsuario;
using GameShelf.Application.CQRS.Commands.CadastrarUsuario;
using GameShelf.Application.CQRS.Commands.DesativarUsuario;
using GameShelf.Application.CQRS.Commands.Login;
using GameShelf.Application.CQRS.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.CQRS.Queries.GetListagemUsuarios;
using GameShelf.Application.CQRS.Queries.GetUsuario;
using GameShelf.Application.DTOs;

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
