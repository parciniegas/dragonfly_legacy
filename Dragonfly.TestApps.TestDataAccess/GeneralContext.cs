using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Dragonfly.Core;
using Dragonfly.Core.Configuration;
using Dragonfly.Core.Security;
using Dragonfly.DataAccess.EF.Base;
using Dragonfly.Domain.Model;

namespace Dragonfly.TestApps.TestDataAccess
{
    public class GeneralContext : BaseContext
    {
        public GeneralContext(IApplicationEnvironment applicationEnvironment) : base(applicationEnvironment)
        {
        }

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
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Role>().Property(p => p.RoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
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
            modelBuilder.Entity<Session>()
                .Property(p => p.SessionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
              modelBuilder.Entity<Course>()
                .Property(p => p.CourseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Instructor>()
                .Property(p => p.InstructorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Schedule>()
                .Property(p => p.ScheduleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Student>()
                .Property(p => p.StudentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            base.OnModelCreating(modelBuilder);
        }
    }
}
