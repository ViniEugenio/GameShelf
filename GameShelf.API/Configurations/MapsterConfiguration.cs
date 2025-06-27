using GameShelf.Application.Commands.CadastrarUsuario;
using GameShelf.Application.Commands.CriarPrateleira;
using GameShelf.Application.DTOs;
using GameShelf.Application.DTOs.UsuarioDTO;
using GameShelf.Application.Queries.GetListagemClaimsUsuarios;
using GameShelf.Application.Queries.GetListagemUsuarios;
using GameShelf.Domain.Entities;
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
            TypeAdapterConfig<BaseEntity, NewRegisterDTO>.NewConfig();
            TypeAdapterConfig<User, NewRegisterDTO>.NewConfig();

            // Custom maps

            TypeAdapterConfig<CadastrarUsuarioCommand, User>
                .NewConfig()
                .Map(

                    destino => destino.UserName,
                    source => source.Email

                );

            TypeAdapterConfig<CriarPrateleiraCommand, Prateleira>
                .NewConfig()
                .Map(

                    destino => destino.Participantes,
                    source => source
                        .Participantes
                        .Select(participante => new ParticipantePrateleira()
                        {
                            UserId = participante
                        })

                );

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        }

    }
}
