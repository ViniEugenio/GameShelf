using GameShelf.Domain.Models.Filters;

namespace GameShelf.Domain.Models.Filters.User
{
    public class GetListagemUsuariosFilter : PaginatedFilterBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime? DataAtivacaoInicio { get; set; }
        public DateTime? DataAtivacaoFim { get; set; }
    }
}
