namespace GameShelf.Application.Queries.GetListagemUsuarios
{
    public class GetListagemUsuariosQuery : PaginatedQueryBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime? DataAtivacaoInicio { get; set; }
        public DateTime? DataAtivacaoFim { get; set; }
    }
}
