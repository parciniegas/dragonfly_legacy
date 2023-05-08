using System;

namespace Dragonfly.Core
{
    public class ExceptionManager
    {
        public void Process(Action action, string message)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new Exception(message, ex);
            }
        }

        public T Process<T>(Func<T> action, string message)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            try
            {
                return action();
            }
            catch (Exception ex)
            {
                throw new Exception(message, ex);
            }
        }
    }
}
