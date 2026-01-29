using GameShelf.Application.DTOs.UsuarioDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GameShelf.API.Configurations
{
    public static class JWTConfiguration
    {

        public static void ConfigureJWTAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {

            var jwtConfig = configuration
                .GetSection("JWTConfiguration")
                .Get<JwtDTO>()!;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.ASCII.GetBytes(jwtConfig.Key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Recusa o token imediatamente quando ele é expirado
                };
            });

        }

    }
}
