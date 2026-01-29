namespace GameShelf.API.Configurations
{
    public static class APIConfiguration
    {

        public static void ConfigureAPI(this IServiceCollection services)
        {
            services.AddControllers();
        }

        public static void ConfigureWebApplication(this WebApplication webApplication)
        {
            webApplication.UseHttpsRedirection();
            webApplication.UseAuthentication();
            webApplication.UseAuthorization();
            webApplication.MapControllers();
            webApplication.Run();
        }

    }
}
