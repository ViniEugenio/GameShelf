using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.ApplicationServices.Services;

namespace GameShelf.API.Configurations
{
    public static class ApplicationServicesConfiguration
    {

        public static void ConfigureApplicationServices(this IServiceCollection services)
        {

            services.AddSingleton<ISessao, Sessao>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPrateleiraService, PrateleiraService>();
            services.AddScoped<IOutboxService, OutboxService>();

        }

    }
}
