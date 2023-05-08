using System;

namespace Dragonfly.Core.Messaging
{
    public class DefaultMessageFormatter: IMessageFormatter
    {
        public string Name => "Default";

        public void Format<T>(IMessage message, T model)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrEmpty(message.Body) || model.Is<string>())
                message.Body = model.ToString();
            else
                message.Body = message.Body.ToString(model);
        }

        public void Format<T>(IMessage message, T model, char prefix)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrEmpty(message.Body) || model.Is<string>())
                message.Body = model.ToString();
            else
                message.Body = message.Body.ToString(model, prefix);
        }
    }
}
