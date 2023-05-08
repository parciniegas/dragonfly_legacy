using Dragonfly.DataAccess.Core;

namespace Dragonfly.Core.Security
{
    public class AuthenticationMethod : Auditable, ITrackeable
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string Assembly { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }
    }
}
