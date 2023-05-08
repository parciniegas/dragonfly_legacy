using System.Collections.Generic;

namespace Dragonfly.Core.Security
{
    public class Option
    {
        public int  OptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public IEnumerable<Action> Actions { get; set; }
    }
}
