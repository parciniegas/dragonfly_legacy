namespace Dragonfly.Core.Security
{
    public class Action
    {
        public int ActionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
