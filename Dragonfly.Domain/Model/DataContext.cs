using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Dragonfly.Core;
using Dragonfly.DataAccess.Core;
using Dragonfly.DataAccess.EF.Base;
using Dragonfly.Core.Configuration;

namespace Dragonfly.Domain.Model
{
    public class DataContext : BaseContext
    {
        #region Constructors
        public DataContext()
            : base("Dragonfly")
        { }

        public DataContext(IApplicationEnvironment applicationEnvironment)
            : base(applicationEnvironment)
        { }

        public DataContext(IApplicationEnvironment applicationEnvironment, ITracker tracker)
            : base(applicationEnvironment, tracker)
        { } 
        #endregion

        #region Properties
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
        }
    }
}
