namespace GameShelf.Domain.Models.Projections.User
{
    public class UsuarioClaimsProjection
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<ClaimProjection> Claims { get; set; } = [];
    }

    public class ClaimProjection
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
    }

}
