using GameShelf.Domain.Enums;

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

        public string Claim { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public PermissoesDTO(string claim, EClaimPermissions permissoes)
        {

            Claim = claim;

            Create = permissoes.HasFlag(EClaimPermissions.Create);
            Read = permissoes.HasFlag(EClaimPermissions.Read);
            Update = permissoes.HasFlag(EClaimPermissions.Update);
            Delete = permissoes.HasFlag(EClaimPermissions.Delete);

        }

    }
}
