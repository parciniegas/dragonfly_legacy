using System;

namespace Dragonfly.DataAccess.Core
{
    public interface ITrack
    {
        int Id { get; set; }
        string EntityName { get; set; }
        string EntityKey { get; set; }
        string Action { get; set; }
        string DataLog { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
