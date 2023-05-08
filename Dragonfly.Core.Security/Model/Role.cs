using System.Collections.Generic;
using Dragonfly.DataAccess.Core;

namespace Dragonfly.Core.Security
{
    public class Role : Auditable, ITrackeable
    {
        #region Properties
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public List<Permission> Permissions { get; set; }
        #endregion Properties
    }
}
