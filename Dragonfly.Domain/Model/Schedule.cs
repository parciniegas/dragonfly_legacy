using System;
using Dragonfly.DataAccess.Core;

namespace Dragonfly.Domain.Model
{
    public class Schedule : Auditable
    {
        public int ScheduleId { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DayTime StarTime { get; set; }
        public DayTime EndTime { get; set; }
    }
}