using GameShelf.Domain.Enums;
using GameShelf.Domain.Models.Projections.User;
using GameShelf.Domain.Models.Security;

namespace GameShelf.Application.DTOs.UsuarioDTO
{
    public class UsuarioClaimsDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<PermissoesDTO> Permissoes { get; set; } = [];
    }

    public class PermissoesDTO
    {

        public int Id { get; set; }
        public string Claim { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public PermissoesDTO(ClaimProjection claim)
        {

            Id = claim.Id;
            Claim = ClaimsManager.ClaimsDescription[claim.Type];

            EClaimPermissions permissoes = (EClaimPermissions)claim.Value;

            Create = permissoes.HasFlag(EClaimPermissions.Create);
            Read = permissoes.HasFlag(EClaimPermissions.Read);
            Update = permissoes.HasFlag(EClaimPermissions.Update);
            Delete = permissoes.HasFlag(EClaimPermissions.Delete);

        }

    }
}
