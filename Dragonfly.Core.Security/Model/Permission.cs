using Dragonfly.DataAccess.Core;

namespace Dragonfly.Core.Security
{
    public class Permission : Auditable, ITrackeable
    {
        #region Properties
        public int PermissionId { get; set; }
        public int ActionId { get; set; }
        public Action Action { get; set; }
        public PermissionType PermissionType { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        #endregion Properties
    }
}
