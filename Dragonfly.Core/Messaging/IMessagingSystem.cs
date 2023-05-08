using System;
using System.Threading.Tasks;

namespace Dragonfly.Core.Messaging
{
    public interface IMessagingSystem
    {
        string Name { get; }
        Type MessageType { get; }
        Task Send(IMessage message);
        Task Send<T>(IMessage message, T model = default(T)) where T : class ;
    }
}
