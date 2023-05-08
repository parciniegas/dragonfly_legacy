using System.Collections.Generic;

namespace Dragonfly.Core.Security
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}
