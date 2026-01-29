using GameShelf.Domain.Models.Filters;

namespace GameShelf.Domain.Models.Filters.User
{
    public class GetListagemClaimsUsuariosFilter : PaginatedFilterBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime? DataAtivacaoInicio { get; set; }
        public DateTime? DataAtivacaoFim { get; set; }
        public List<string> ClaimsTypes { get; set; } = [];
    }
}
