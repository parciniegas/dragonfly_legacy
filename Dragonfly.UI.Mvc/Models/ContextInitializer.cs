using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dragonfly.Domain.Model;

namespace Dragonfly.UI.Mvc.Models
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
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
