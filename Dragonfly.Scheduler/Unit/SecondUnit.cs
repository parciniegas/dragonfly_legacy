namespace Dragonfly.Scheduler
{
    /// <summary>
    /// Unit of time in seconds.
    /// </summary>
    public sealed class SecondUnit : ITimeRestrictableUnit
    {
        internal SecondUnit(Schedule schedule, int duration)
        {
            Schedule = schedule;
            Schedule.CalculateNextRun = x => x.AddSeconds(duration);
        }

        internal Schedule Schedule { get; private set; }

        Schedule ITimeRestrictableUnit.Schedule => Schedule;
    }
}
