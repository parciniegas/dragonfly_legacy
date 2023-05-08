namespace Dragonfly.DataAccess.Core
{
    // ReSharper disable once InconsistentNaming
    public abstract class Entity<K> : IEntity<K>
    {
        public K Id { get; set; }
    }
}
