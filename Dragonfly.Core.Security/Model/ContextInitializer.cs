using System.Collections.Generic;
using System.Data.Entity;

namespace Dragonfly.Core.Security
{
    internal class ContextInitializer : CreateDatabaseIfNotExists<SecurityContext>
    {
        protected override void Seed(SecurityContext context)
        {
            var admin = new User
            {
                UserId = 1,
                LoginName = "admin",
                Password = PasswordHash.CreateHash("SysAdmin"),
                OtpKey = "A181A444FDFA40F1840C7BD477973AB8",
                RequireOtp = false,
                OtpSendMode = OtpSendMode.None,
                FirstName = "System",
                LastName = "Administrator",
                Enabled = EnabledState.EnabledForever,
                AuthenticationMethod = new AuthenticationMethod
                {
                    Id = 1,
                    Method = "SqlAuthentication",
                    Class = "Dragonfly.Core.Security.SqlAuthentication",
                    Assembly = "Dragonfly.Core.Security",
                    Description = "Sql Server authentication"
                },
                ConnectionTries = 99,
                IsApproved = true,
                IsLocked = false,
                IsOnline = false,
                MustChangePassword = false,
                Roles = new List<Role>
                {
                    new Role
                    {
                        RoleId = 1,
                        Name = "Administrators",
                        Description = "System Administrators"
                    }
                }
            };

            var guest = new User
            {
                UserId = 1,
                LoginName = "guest",
                Password = PasswordHash.CreateHash("GuestUser"),
                OtpKey = "04C2C1166E8E4F3494A80012F319F249",
                RequireOtp = false,
                OtpSendMode = OtpSendMode.None,
                FirstName = "Guest",
                LastName = "User",
                Enabled = EnabledState.EnabledForever,
                AuthenticationMethod = new AuthenticationMethod
                {
                    Id = 1,
                    Method = "SqlAuthentication",
                    Class = "Dragonfly.Core.Security.SqlAuthentication",
                    Assembly = "Dragonfly.Core.Security",
                    Description = "Sql Server authentication"
                },
                ConnectionTries = 99,
                IsApproved = true,
                IsLocked = false,
                IsOnline = false,
                MustChangePassword = false,
                Roles = new List<Role>
                {
                    new Role
                    {
                        RoleId = 1,
                        Name = "Guests",
                        Description = "System Guests"
                    }
                }
            };

            context.Users.Add(admin);
            context.Users.Add(guest);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
