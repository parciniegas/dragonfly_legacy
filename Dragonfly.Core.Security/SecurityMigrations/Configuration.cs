using System.Collections.Generic;

namespace Dragonfly.Core.Security.SecurityMigrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SecurityMigrations";
        }

        protected override void Seed(SecurityContext context)
        {
            //var admin = new User
            //{
            //    UserId = 1,
            //    LoginName = "admin",
            //    Password = PasswordHash.CreateHash("SysAdmin"),
            //    FirstName = "System",
            //    LastName = "Administrator",
            //    Enabled = EnabledState.EnabledForever,
            //    AuthenticationMethod = new AuthenticationMethod
            //    {
            //        Id = 1,
            //        Method = "SqlAuthentication",
            //        Class = "Dragonfly.Core.Security.SqlAuthentication",
            //        Assembly = "Dragonfly.Core.Security",
            //        Description = "Sql Server authentication"
            //    },
            //    ConnectionTries = 99,
            //    IsApproved = true,
            //    IsLocked = false,
            //    IsOnline = false,
            //    MustChangePassword = false,
            //    Roles = new List<Role>
            //    {
            //        new Role
            //        {
            //            RoleId = 1,
            //            Name = "Administrators",
            //            Description = "System Administrators"
            //        }
            //    }
            //};

            //context.Users.Add(admin);
            //context.SaveChanges();

            //base.Seed(context);
        }
    }
}
