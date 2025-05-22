namespace GameShelf.Application.DTOs.UsuarioDTO
{
    public class LoginDTO
    {
        public string JWT { get; set; }
        public UsuarioLoginDTO Usuario { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
