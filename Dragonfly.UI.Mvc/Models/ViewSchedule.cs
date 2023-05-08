using System;
using Dragonfly.Domain.Model;

namespace Dragonfly.UI.Mvc.Models
{
    public class ViewSchedule
    {
        public int ScheduleId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DayTime StartTime { get; set; }
        public DayTime EndTime { get; set; }
    }
}