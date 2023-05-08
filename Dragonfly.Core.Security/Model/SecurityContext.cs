using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Dragonfly.Core.Configuration;
using Dragonfly.DataAccess.EF.Base;

namespace Dragonfly.Core.Security
{
    internal class SecurityContext : BaseContext
    {
        #region Constructors

        public SecurityContext()
            : base("Dragonfly")
        { }

        public SecurityContext(IApplicationEnvironment environment)
            : base(environment)
        { }
        #endregion Constructors

        #region Public Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Restrictions> Restrictions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<NetworkSegment> NetworkSegments { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<AuthenticationMethod> AuthenticationMethods { get; set; }
        #endregion Public Properties

        #region Overridden Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>()
                .Property(p => p.ActionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<AuthenticationMethod>()
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Module>()
                .Property(p => p.ModuleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<NetworkSegment>()
                .Property(p => p.NetworkSegmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Option>()
                .Property(p => p.OptionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Permission>()
                .Property(p => p.PermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Restrictions>()
                .Property(p => p.RestrictionsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Role>()
                .Property(p => p.RoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Session>()
                .Property(p => p.SessionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<User>()
                .Property(p => p.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
