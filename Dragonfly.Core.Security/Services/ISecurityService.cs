using System.Collections.Generic;

namespace Dragonfly.Core.Security.Services
{
    public interface ISecurityService
    {
        Role GetRol(int id);
        User GetUser(int id);
        
        IEnumerable<User> GetUsers();
        IEnumerable<Role> GetRoles();
        IEnumerable<Module> GetModules();
        void AddUser(User user);
        void AddRole(Role role);
        void AddModule(Module module);
        void UpdateUser(User user);
        void UpdateRole(Role role);
        void UpdateModule(Module module);
        void DeleteUser(User user);
        void DeleteRole(Role role);
        void DeleteModule(Module module);
    }
}
