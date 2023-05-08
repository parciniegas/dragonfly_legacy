namespace Dragonfly.Core.Readers
{
    public interface IParserConfiguration
    {
        string Delimiter { get; set; }
        bool HasHeader { get; set; }
    }
}
