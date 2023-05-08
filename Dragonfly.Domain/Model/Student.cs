using Dragonfly.DataAccess.Core;

namespace Dragonfly.Domain.Model
{
    public class Student : Auditable
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }
    }
}