using System;

namespace Dragonfly.UI.Mvc.Models
{
    public class ViewCourse
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int? InstructorId { get; set; }
        public string InstructorName { get; set; }
        //public IEnumerable<ViewSchedule> ViewSchedules { get; set; }
        //public IEnumerable<ViewStudent> ViewStudents { get; set; }
    }
}
