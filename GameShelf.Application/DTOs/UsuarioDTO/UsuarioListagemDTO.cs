namespace GameShelf.Application.DTOs.UsuarioDTO
{

    public class UsuarioListagemDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAtivacao { get; set; }
        public DateTime? DataDesativacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }

}
