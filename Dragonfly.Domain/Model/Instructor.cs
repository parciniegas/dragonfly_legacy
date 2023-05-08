using System;
using System.Collections.Generic;
using Dragonfly.DataAccess.Core;

namespace Dragonfly.Domain.Model
{
    public class Instructor : Auditable
    {
        public int InstructorId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}