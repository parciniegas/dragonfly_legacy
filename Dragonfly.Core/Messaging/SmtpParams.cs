using System.Net.Mail;

namespace Dragonfly.Core.Messaging
{
    public class SmtpParams
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public MailPriority Priority { get; set; }
        public string UserName { get; set; }
        public string Password  { get; set; }
        public bool UseSsl { get; set; }
    }
}
