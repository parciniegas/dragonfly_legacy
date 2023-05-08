namespace Dragonfly.DataAccess.Core
{
    // ReSharper disable once InconsistentNaming
    public interface IEntity<K>
    {
        K Id { get; set; }
    }
}
