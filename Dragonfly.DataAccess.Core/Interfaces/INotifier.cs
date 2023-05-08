namespace Dragonfly.DataAccess.Core
{
    public interface INotifier<in T>
    {
        void Notify(T entity);
    }
}
