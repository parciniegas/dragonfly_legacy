using Dragonfly.Core.Common;

namespace Dragonfly.Core.Messaging
{
    public abstract class BaseMessage : Disposable, IMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
