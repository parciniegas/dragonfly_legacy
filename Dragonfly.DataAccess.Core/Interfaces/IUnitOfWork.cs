namespace Dragonfly.DataAccess.Core
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        // ReSharper disable once InconsistentNaming
        IRepository<T> Repository<T>() where T : class;
        void SetAutoDetectChanges(bool enabled);
    }
}
