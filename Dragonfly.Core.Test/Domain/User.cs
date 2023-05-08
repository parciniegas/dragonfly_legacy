using Dragonfly.Core.Attributes;

namespace Dragonfly.Core.Test.Domain
{
    internal class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [Encrypt]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
