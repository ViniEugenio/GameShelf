using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Application.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Domain.Filters.User;
using GameShelf.Domain.Projections.User;
using Mapster;
using System.Reflection;

namespace GameShelf.API.Configurations
{
    public static class MapsterConfiguration
    {

        public static void ConfigureMapster(this IServiceCollection services)
        {

            TypeAdapterConfig<UsuarioPaginacaoProjection, UsuarioListagemDTO>.NewConfig();
            TypeAdapterConfig<UsuarioSimplificadoProjection, UsuarioSimplificadoDTO>.NewConfig();
            TypeAdapterConfig<UsuarioLoginProjection, UsuarioLoginDTO>.NewConfig();
            TypeAdapterConfig<GetListagemClaimsUsuariosQuery, GetListagemClaimsUsuariosFilter>.NewConfig();
            TypeAdapterConfig<GetListagemUsuariosQuery, GetListagemUsuariosFilter>.NewConfig();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        }

    }
}
