using Scalar.AspNetCore;

namespace GameShelf.API.Configurations
{
    public static class DocumentationConfiguration
    {

        public static void ConfigureDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi();
        }

        public static void ConfigureWebApplicationDocumentation(this WebApplication app)
        {

            if (!app.Environment.IsDevelopment())
            {
                return;
            }

            app.MapOpenApi();
            app.MapScalarApiReference();

        }

    }
}
