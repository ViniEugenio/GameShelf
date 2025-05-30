namespace GameShelf.Domain.Security
{
    public static class ClaimsManager
    {

        public const string Admin = "Admin";
        public const string UserAdmin = "UserAdmin";
        public const string User = "User";

        public static readonly Dictionary<string, string> ClaimsDescription = new()
        {
            { Admin, "Administrador Geral" },
            { UserAdmin, "Administrador de Usuários" }
        };

    }
}
