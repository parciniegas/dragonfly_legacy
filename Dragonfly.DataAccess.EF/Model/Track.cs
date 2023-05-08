using System;
using Dragonfly.DataAccess.Core;

namespace Dragonfly.DataAccess.EF.Model
{
    public class Track : ITrack
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string EntityKey { get; set; }
        public string Action { get; set; }
        public string DataLog { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
