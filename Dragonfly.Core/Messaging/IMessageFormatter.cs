namespace Dragonfly.Core.Messaging
{
    public interface IMessageFormatter
    {
        string Name { get; }
        void Format<T>(IMessage message, T model);
        void Format<T>(IMessage message, T model, char prefix);
    }
}
