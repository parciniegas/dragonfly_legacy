using System.Collections.Generic;
using System.Net.Mail;

namespace Dragonfly.Core.Messaging
{
    public class EmailMessage : BaseMessage
    {
        #region Constructors

        public EmailMessage()
        {
            Attachments = new List<Attachment>();
            LinkedResources = new List<LinkedResource>();
        }
        #endregion

        #region Properties
        public ICollection<Attachment> Attachments { get; private set; }
        public ICollection<LinkedResource> LinkedResources { get; private set; }
        public string Bcc { get; set; }
        public string Cc { get; set; }
        #endregion

        protected override void Dispose(bool disposed)
        {
            if (Attachments != null)
            {
                foreach (var attachment in Attachments)
                {
                    attachment.Dispose();
                }
                Attachments = null;
            }

            if (LinkedResources != null)
            {
                foreach (var resource in LinkedResources)
                {
                    resource.Dispose();
                }
                LinkedResources = null;
            }
        }
    }
}
