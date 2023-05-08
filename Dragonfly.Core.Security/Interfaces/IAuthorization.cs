using System.Collections.Generic;

namespace Dragonfly.Core.Security
{
    public interface IAuthorization
    {
        IEnumerable<Module> GetAuthorizedModules(string user);
        IEnumerable<Option> GetAuthorizedOptions(string user);
        IEnumerable<Action> GetAuthorizedActions(string user);
        bool AuthorizeAction(string user, string action);
    }
}
