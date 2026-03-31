namespace Anatta.Framework.Threading;

public class ScheduledDelegate : IComparable<ScheduledDelegate>
{
    public VoidDelegate Task;

    public float WaitTime;

    public float RepeatInterval;

    public bool Cancelled { get; private set; }

    public ScheduledDelegate(VoidDelegate task, float waitTime, float repeatInterval = 0)
    {
        WaitTime = waitTime;
        RepeatInterval = repeatInterval;
        Task = task;
    }

    public void Cancel()
    {
        Cancelled = true;
    }

    public int CompareTo(ScheduledDelegate other)
    {
        if (WaitTime != other.WaitTime)
        {
            return WaitTime.CompareTo(other.WaitTime);
        }
        return -1;
    }
}