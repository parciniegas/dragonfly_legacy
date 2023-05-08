using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dragonfly.Core.Configuration;

namespace Dragonfly.Core.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly SecurityContext _context;

        public SecurityService(IApplicationEnvironment applicationEnvironment)
        {
            _context = new SecurityContext(applicationEnvironment);
        }

        public Role GetRol(int id)
        {
            return _context.Roles.Find(id);
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users
                .Include(u => u.Roles.Select(p => p.Permissions))
                .Include(u => u.PasswordHistory);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles
                .Include(r => r.Users)
                .Include(r => r.Permissions);
        }

        public IEnumerable<Module> GetModules()
        {
            return _context.Modules
                .Include(m => m.Options.Select(o => o.Actions));
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void AddModule(Module module)
        {
            _context.Modules.Add(module);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void UpdateModule(Module module)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void DeleteModule(Module module)
        {
            throw new NotImplementedException();
        }
    }
}
