using System;

namespace Dragonfly.Core.Security
{
    public class Session
    {
        public string SessionId { get; set; }
        public string HostAddress { get; set; }
        public string HostName { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public string UserAgent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
