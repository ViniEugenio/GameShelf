﻿using GameShelf.Application.DTOs.UsuarioDTO;

namespace GameShelf.API.Configurations
{
    public static class IOptionsConfiguration
    {

        public static void ConfigureIOptionsConfigurations(this IServiceCollection services, ConfigurationManager configuration)
        {

            services
                .Configure<JwtDTO>(configuration.GetSection("JWTConfiguration"));

        }

    }
}
