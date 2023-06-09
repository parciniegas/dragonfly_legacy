﻿namespace Dragonfly.Scheduler
{
    /// <summary>
    /// Unit of time that represents a specific day of the month.
    /// </summary>
    public sealed class MonthOnDayOfMonthUnit
    {
        private readonly int _duration;

        private readonly int _dayOfMonth;

        internal MonthOnDayOfMonthUnit(Schedule schedule, int duration, int dayOfMonth)
        {
            _duration = duration;
            _dayOfMonth = dayOfMonth;
            Schedule = schedule;
            At(0, 0);
        }

        internal Schedule Schedule { get; private set; }

        /// <summary>
        /// Runs the job at the given time of day.
        /// </summary>
        /// <param name="hours">The hours (0 through 23).</param>
        /// <param name="minutes">The minutes (0 through 59).</param>
        public void At(int hours, int minutes)
        {
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.First().AddDays(_dayOfMonth - 1).AddHours(hours).AddMinutes(minutes);
                return x > nextRun ? x.Date.First().AddMonths(_duration).AddDays(_dayOfMonth - 1).AddHours(hours).AddMinutes(minutes) : nextRun;
            };
        }
    }
}
