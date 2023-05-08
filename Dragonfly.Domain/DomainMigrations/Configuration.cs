using System.Collections.Generic;
using Dragonfly.Domain.Model;

namespace Dragonfly.Domain.DomainMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DomainMigrations";
        }

        protected override void Seed(DataContext context)
        {
            //var instructors = new List<Instructor>
            //{
            //    new Instructor {InstructorId = Guid.NewGuid(), Name = "Carl Sagan"},
            //    new Instructor {InstructorId = Guid.NewGuid(), Name = "Marie Courie"},
            //    new Instructor {InstructorId = Guid.NewGuid(), Name = "Albert Einstein"},
            //    new Instructor {InstructorId = Guid.NewGuid(), Name = "Isaac Asimov"}
            //};

            //var courses = new List<Course>
            //{
            //    new Course {CourseId = Guid.NewGuid(), Name = "Introducci�n a la astronom�a", InstructorId = instructors.First().InstructorId },
            //    new Course {CourseId = Guid.NewGuid(), Name = "Din�mica I", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId },
            //    new Course {CourseId = Guid.NewGuid(), Name = "Din�mica II", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId },
            //    new Course {CourseId = Guid.NewGuid(), Name = "Termodin�mica I", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Termodin�mica II", InstructorId = instructors.First(i => i.Name == "Marie Courie").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Relatividad", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Mec�nica Cu�ntica", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Mec�nica", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Electromagnetismo", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Oscilaciones y Ondas", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Ciencias Planetarias", InstructorId = instructors.First(i => i.Name == "Isaac Asimov").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Mec�nica Celeste", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Mec�nica de Medios Continuos", InstructorId = instructors.First(i => i.Name == "Albert Einstein").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Astrofisica Moderna", InstructorId = instructors.First(i => i.Name == "Carl Sagan").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Astrof�sica Estelar", InstructorId = instructors.First(i => i.Name == "Carl Sagan").InstructorId},
            //    new Course {CourseId = Guid.NewGuid(), Name = "Galaxias y Cosmolog�a", InstructorId = instructors.First(i => i.Name == "Carl Sagan").InstructorId}
            //};

            //var students = new List<Student>
            //{
            //    new Student {StudentId = Guid.NewGuid(), FirstName = "Mario", LastName = "L�pez"},
            //    new Student {StudentId = Guid.NewGuid(), FirstName = "Carlos", LastName = "Sanchez"},
            //    new Student {StudentId = Guid.NewGuid(), FirstName = "Martha", LastName = "P�rez"},
            //    new Student {StudentId = Guid.NewGuid(), FirstName = "Gloria", LastName = "Gaynor"}
            //};

            //instructors.ForEach(i => context.Instructors.Add(i));
            //courses.ForEach(c => context.Courses.Add(c));
            //students.ForEach(e => context.Students.Add(e));

            //context.SaveChanges();
        }
    }
}
