using System.Collections.Generic;
using System.Linq;
using Dragonfly.Core.Configuration;

namespace Dragonfly.Core.Security
{
    public class Authorization : IAuthorization
    {
        #region Private Fields
        private readonly SecurityContext _context;
        #endregion Private Fields

        #region Constructors
        public Authorization(IApplicationEnvironment applicationEnvironment)
        {
            _context = new SecurityContext(applicationEnvironment);
        }
        #endregion

        #region Public Methods
        public IEnumerable<Module> GetAuthorizedModules(string user)
        {
            var query = from m in _context.Modules
                        join o in _context.Options on m.ModuleId equals o.ModuleId
                        join a in _context.Actions on o.OptionId equals a.OptionId
                        join p in _context.Permissions on a.ActionId equals p.ActionId
                        join r in _context.Roles on p.RoleId equals r.RoleId
                        where r.Users.Exists(u => u.LoginName == user)
                        where p.PermissionType.Equals(PermissionType.Allow)
                        select m;

            return query;
        }

        public IEnumerable<Option> GetAuthorizedOptions(string user)
        {
            var query = from o in _context.Options
                        join a in _context.Actions on o.OptionId equals a.OptionId
                        join p in _context.Permissions on a.ActionId equals p.ActionId
                        join r in _context.Roles on p.RoleId equals r.RoleId
                        where r.Users.Exists(u => u.LoginName == user)
                        where p.PermissionType.Equals(PermissionType.Allow)
                        select o;

            return query;
        }

        public IEnumerable<Action> GetAuthorizedActions(string user)
        {
            var query = from a in _context.Actions
                        join p in _context.Permissions on a.ActionId equals p.ActionId
                        join r in _context.Roles on p.RoleId equals r.RoleId
                        where r.Users.Exists(u => u.LoginName == user)
                        where p.PermissionType.Equals(PermissionType.Allow)
                        select a;

            return query;
        }

        public bool AuthorizeAction(string user, string action)
        {
            var query = from a in _context.Actions
                        join p in _context.Permissions on a.ActionId equals p.ActionId
                        join r in _context.Roles on p.RoleId equals r.RoleId
                        where r.Users.Exists(u => u.LoginName == user)
                        where p.PermissionType.Equals(PermissionType.Allow)
                        where a.Name.Equals(action)
                        select a;
            return query.Any();
        }
        #endregion
    }
}
