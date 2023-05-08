using Dragonfly.Core.Readers;

namespace Dragonfly.FlatFileHelper
{
    public class ParserConfiguration : IParserConfiguration
    {
        public string Delimiter { get; set; } = ";";
        public bool HasHeader { get; set; } = false;
    }
}
