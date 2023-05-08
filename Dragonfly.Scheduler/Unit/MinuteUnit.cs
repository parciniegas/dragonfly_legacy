namespace Dragonfly.Scheduler
{
    /// <summary>
    /// Unit of time in minutes.
    /// </summary>
    public sealed class MinuteUnit : ITimeRestrictableUnit
    {
        internal MinuteUnit(Schedule schedule, int duration)
        {
            Schedule = schedule;
            Schedule.CalculateNextRun = x => x.AddMinutes(duration);
        }

        internal Schedule Schedule { get; private set; }

        Schedule ITimeRestrictableUnit.Schedule => Schedule;
    }
}
