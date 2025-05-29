namespace GameShelf.Application.DTOs.UsuarioDTO
{
    public class JwtDTO
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireHours { get; set; }
    }
}