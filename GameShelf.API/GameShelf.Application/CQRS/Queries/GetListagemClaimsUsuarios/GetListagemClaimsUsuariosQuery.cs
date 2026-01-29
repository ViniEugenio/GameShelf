using GameShelf.Application.CQRS.Queries;

namespace GameShelf.Application.CQRS.Queries.GetListagemClaimsUsuarios
{
    public class GetListagemClaimsUsuariosQuery : PaginatedQueryBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime? DataAtivacaoInicio { get; set; }
        public DateTime? DataAtivacaoFim { get; set; }
        public List<string> ClaimTypes { get; set; } = [];
    }
}
