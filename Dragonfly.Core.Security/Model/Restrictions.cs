using System.Collections.Generic;
using Dragonfly.DataAccess.Core;

namespace Dragonfly.Core.Security
{
    public class Restrictions : Auditable, ITrackeable
    {
        #region Constructors
        public Restrictions()
        {
            AllowedNetworkSegments = new List<NetworkSegment>();
        } 
        #endregion Constructors

        #region Public Properties
        public int RestrictionsId { get; set; }
        public int MaxConnectedUsers { get; set; }
        public int MaxSessionsPerUser { get; set; }
        // ReSharper disable once MemberCanBePrivate.Global
        public List<NetworkSegment> AllowedNetworkSegments { get; private set; }
        #endregion Public Properties
    }
}
