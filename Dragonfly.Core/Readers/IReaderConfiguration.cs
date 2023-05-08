namespace Dragonfly.Core.Readers
{
    public interface IReaderConfiguration
    {
        bool HasHeaders { get; set; }
        string ResourceName { get; set; }
    }
}
