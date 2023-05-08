using Dragonfly.DataAccess.Core;
using System.Collections.Generic;

namespace Dragonfly.Domain.Model
{
    public class Course : Auditable
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int? InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}