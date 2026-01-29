using System.ComponentModel;

namespace GameShelf.Domain.Enums
{
    [Flags]
    public enum EClaimPermissions
    {

        None = 0,

        [Description("create")]
        Create = 1 << 0,

        [Description("read")]
        Read = 1 << 1,

        [Description("update")]
        Update = 1 << 2,

        [Description("delete")]
        Delete = 1 << 3

    }
}
