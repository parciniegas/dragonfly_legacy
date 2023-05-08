using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dragonfly.Core.Security;
using Dragonfly.Domain.Model;

namespace Dragonfly.TestApps.TestDataAccess
{
    public class ContextInitializer : DropCreateDatabaseAlways<GeneralContext>
    {
        protected override void Seed(GeneralContext context)
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
                UserId = 2,
                LoginName = "guest",
                Password = PasswordHash.CreateHash("GuestUser"),
                OtpKey = "04C2C1166E8E4F3494A80012F319F249",
                RequireOtp = false,
                OtpSendMode = OtpSendMode.None,
                FirstName = "Guest",
                LastName = "User",
                Enabled = EnabledState.EnabledForever,
                AuthenticationMethodId = 1,
                ConnectionTries = 99,
                IsApproved = true,
                IsLocked = false,
                IsOnline = false,
                MustChangePassword = false,
                Roles = new List<Role>
                {
                    new Role
                    {
                        RoleId = 2,
                        Name = "Guests",
                        Description = "System Guests"
                    }
                }
            };

            context.Users.Add(admin);
            context.Users.Add(guest);
            context.SaveChanges();

            base.Seed(context);

            var instructors = new List<Instructor>
            {
                new Instructor {InstructorId = 1, Name = "Carl Sagan"},
                new Instructor {InstructorId = 2, Name = "Marie Courie"},
                new Instructor {InstructorId = 3, Name = "Albert Einstein"},
                new Instructor {InstructorId = 4, Name = "Isaac Asimov"}
            };

            var courses = new List<Course>
            {
                new Course {CourseId = 1, Name = "Introducción a la astronomía", InstructorId = instructors.First().InstructorId },
                new Course {CourseId = 2, Name = "Dinámica I", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId },
                new Course {CourseId = 3, Name = "Dinámica II", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId },
                new Course {CourseId = 4, Name = "Termodinámica I", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId},
                new Course {CourseId = 5, Name = "Termodinámica II", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId},
                new Course {CourseId = 6, Name = "Relatividad", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
                new Course {CourseId = 7, Name = "Mecánica Cuántica", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
                new Course {CourseId = 8, Name = "Mecánica", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
                new Course {CourseId = 9, Name = "Electromagnetismo", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
                new Course {CourseId = 10, Name = "Oscilaciones y Ondas", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
                new Course {CourseId = 11, Name = "Ciencias Planetarias", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
                new Course {CourseId = 12, Name = "Mecánica Celeste", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
                new Course {CourseId = 13, Name = "Mecánica de Medios Continuos", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
                new Course {CourseId = 14, Name = "Astrofisica Moderna", InstructorId = instructors.First(i => i.Name == "Carl Sagan").InstructorId},
                new Course {CourseId = 15, Name = "Astrofísica Estelar", InstructorId = instructors.First(i => i.Name == "Carl Sagan").InstructorId},
                new Course {CourseId = 16, Name = "Galaxias y Cosmología", InstructorId = instructors.First(i => i.Name == "Carl Sagan").InstructorId}
            };

            var students = new List<Student>
            {
                new Student {StudentId = 1, FirstName = "Mario", LastName = "López"},
                new Student {StudentId = 2, FirstName = "Carlos", LastName = "Sanchez"},
                new Student {StudentId = 3, FirstName = "Martha", LastName = "Pérez"},
                new Student {StudentId = 4, FirstName = "Gloria", LastName = "Gaynor"}
            };

            instructors.ForEach(i => context.Instructors.Add(i));
            courses.ForEach(c => context.Courses.Add(c));
            students.ForEach(e => context.Students.Add(e));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
