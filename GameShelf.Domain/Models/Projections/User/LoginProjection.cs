using System.Security.Claims;

namespace GameShelf.Domain.Models.Projections.User
{
    public class LoginProjection
    {
        public ClaimsIdentity Claims { get; set; }
        public UsuarioLoginProjection Usuario { get; set; }
    }

    public class UsuarioLoginProjection
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
