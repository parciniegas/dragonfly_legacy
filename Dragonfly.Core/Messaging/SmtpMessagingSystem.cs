using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;

namespace Dragonfly.Core.Messaging
{
    public class SmtpMessagingSystem : BaseMessagingSystem
    {
        #region Private Fields
        private readonly SmtpParams _smtpParams;
        #endregion

        public SmtpMessagingSystem(ISmtpConfigurator smtpConfigurator,
            IEnumerable<IMessageFormatter> formatters) : base(formatters)
        {
            Contract.Requires<ArgumentNullException>(smtpConfigurator != null, nameof(smtpConfigurator));
            _smtpParams = smtpConfigurator.GetParams();
        }

        public override string Name => "SMTP";
        public override Type MessageType => typeof(EmailMessage);

        protected override void InternalSend(IMessage message)
        {
            var emailMessage = message as EmailMessage;
            if (emailMessage == null)
                return;

            if (string.IsNullOrEmpty(emailMessage.Body))
                emailMessage.Body = " ";

            using (var mailMessage = new MailMessage())
            {
                char[] splitter = { ',', ';' };
                var addressCollection = emailMessage.To.Split(splitter);
                foreach (var t in addressCollection.Where(t => !string.IsNullOrEmpty(t.Trim())))
                {
                    mailMessage.To.Add(t);
                }

                if (!string.IsNullOrEmpty(emailMessage.Cc))
                {
                    addressCollection = emailMessage.Cc.Split(splitter);
                    foreach (var t in addressCollection.Where(t => !string.IsNullOrEmpty(t.Trim())))
                    {
                        mailMessage.CC.Add(t);
                    }
                }

                if (!string.IsNullOrEmpty(emailMessage.Bcc))
                {
                    addressCollection = emailMessage.Bcc.Split(splitter);
                    foreach (var t in addressCollection.Where(t => !string.IsNullOrEmpty(t.Trim())))
                    {
                        mailMessage.Bcc.Add(t);
                    }
                }

                mailMessage.Subject = emailMessage.Subject;
                if (!string.IsNullOrEmpty(emailMessage.From))
                    mailMessage.From = new MailAddress(emailMessage.From);

                using (var bodyView = AlternateView.CreateAlternateViewFromString(emailMessage.Body, null, MediaTypeNames.Text.Html))
                {
                    foreach (var resource in emailMessage.LinkedResources.Check(new List<LinkedResource>()))
                    {
                        bodyView.LinkedResources.Add(resource);
                    }

                    mailMessage.AlternateViews.Add(bodyView);
                    mailMessage.Priority = _smtpParams.Priority;
                    mailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    mailMessage.IsBodyHtml = true;
                    foreach (var attachment in emailMessage.Attachments.Check(new List<Attachment>()))
                    {
                        mailMessage.Attachments.Add(attachment);
                    }

                    SendMessage(
                        !string.IsNullOrEmpty(_smtpParams.Server)
                            ? new SmtpClient(_smtpParams.Server, _smtpParams.Port)
                            : new SmtpClient(), _smtpParams, mailMessage);
                }
            }
        }

        #region Private Methods
        private static void SendMessage(SmtpClient smtpClient, SmtpParams smtpParams, MailMessage mailMessage)
        {
            Contract.Requires<ArgumentNullException>(smtpParams != null, nameof(smtpParams));
            Contract.Requires<ArgumentNullException>(smtpClient != null, nameof(smtpClient));
            Contract.Requires<ArgumentNullException>(mailMessage != null, nameof(mailMessage));
            using (var smtp = smtpClient)
            {
                if (!string.IsNullOrEmpty(smtpParams.UserName) && !string.IsNullOrEmpty(smtpParams.Password))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(smtpParams.UserName, smtpParams.Password);
                }
                smtp.EnableSsl = smtpParams.UseSsl;
                smtp.Send(mailMessage);
            }
        }
        #endregion
    }
}
