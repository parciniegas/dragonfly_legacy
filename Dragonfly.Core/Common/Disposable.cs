using System;

namespace Dragonfly.Core.Common
{
    public abstract class Disposable : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposed);

        ~Disposable()
        {
            Dispose(false);
        }
    }
}
