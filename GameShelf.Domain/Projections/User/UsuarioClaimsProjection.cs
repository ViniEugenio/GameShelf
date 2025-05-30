using System.Security.Claims;

namespace GameShelf.Domain.Projections.User
{
    public class UsuarioClaimsProjection
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Claim> Claims { get; set; } = [];
    }

    public class UsuarioClaimFilterProjection : PaginatedFilterBaseProjection
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime? DataAtivacaoInicio { get; set; }
        public DateTime? DataAtivacaoFim { get; set; }
        public List<string> ClaimsTypes { get; set; }
    }
}
