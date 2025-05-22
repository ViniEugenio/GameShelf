using GameShelf.Application.DTOs.UsuarioDTO;
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

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        }

    }
}
