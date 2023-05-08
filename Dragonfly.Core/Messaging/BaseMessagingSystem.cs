using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dragonfly.Core.Messaging
{
    public abstract class BaseMessagingSystem : IMessagingSystem
    {
        #region Private Fields
        private IEnumerable<IMessageFormatter> Formatters { get; }
        #endregion

        #region Constructors
        protected BaseMessagingSystem(IEnumerable<IMessageFormatter> formatters)
        {
            Formatters = formatters;
        }
        #endregion

        #region Properties
        public abstract string Name { get; }
        public abstract Type MessageType { get; }

        #endregion

        public Task Send(IMessage message)
        {
            throw new NotImplementedException();
        }

        public async Task Send<T>(IMessage message, T model = default(T)) where T : class
        {
            if (message == null)
                return;

            await Task.Run(() =>
            {
                if (model != null)
                {
                    foreach (var formatter in Formatters)
                    {
                        formatter.Format(message, model);
                    }
                }
                InternalSend(message);
            });
        }

        protected abstract void InternalSend(IMessage message);
    }
}
