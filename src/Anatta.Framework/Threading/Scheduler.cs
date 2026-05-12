using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Anatta.Framework.Logging;

namespace Anatta.Framework.Threading;

public class Scheduler {
	private Logger logger = new Logger("Scheduler");
	
	private readonly Queue<Action> schedulerQueue = new Queue<Action>();

	private readonly List<ScheduledDelegate> timedTasks = new List<ScheduledDelegate>();

	private int mainThreadId;
	protected virtual bool isMainThread => Thread.CurrentThread.ManagedThreadId == mainThreadId;

	public Scheduler()
	{
		mainThreadId = Thread.CurrentThread.ManagedThreadId;
	}

	public bool Update()
	{
		Action[] array;
		lock (schedulerQueue)
		{
			lock (timedTasks)
			{
				float elapsedMilliseconds = Time.Total;
				ScheduledDelegate scheduledDelegate;
				while (timedTasks.Count > 0 && (scheduledDelegate = timedTasks[0]).WaitTime <= elapsedMilliseconds)
				{
					timedTasks.RemoveAt(0);
					if (!scheduledDelegate.Cancelled)
					{
						schedulerQueue.Enqueue(scheduledDelegate.Task);
						if (scheduledDelegate.RepeatInterval > 0)
						{
							scheduledDelegate.WaitTime = Time.Total + scheduledDelegate.RepeatInterval;
							timedTasks.Add(scheduledDelegate);
						}
					}
				}
			}
			int count = schedulerQueue.Count;
			if (count == 0)
			{
				return false;
			}
			array = new Action[count];
			schedulerQueue.CopyTo(array, 0);
			schedulerQueue.Clear();
		}
		Action[] array2 = array;
		foreach (Action voidDelegate in array2)
		{
			try
			{
				new Action(voidDelegate.Invoke)();
			}
			catch (Exception arg)
			{
				logger.Error($"Error in scheduled task: {arg}");
			}
		}
		return true;
	}

	public virtual bool Add(Action task, bool forceScheduled = false)
	{
		if (!forceScheduled && isMainThread)
		{
			task();
			return true;
		}
		lock (schedulerQueue)
		{
			schedulerQueue.Enqueue(task);
		}
		return false;
	}

	public ScheduledDelegate AddDelayed(Action task, float timeUntilRun, bool repeat = false)
	{
		ScheduledDelegate scheduledDelegate = new ScheduledDelegate(task, Time.Total + timeUntilRun, repeat ? timeUntilRun : 0);
		lock (timedTasks)
		{
			timedTasks.Add(scheduledDelegate);
			return scheduledDelegate;
		}
	}

	public bool AddOnce(Action task)
	{
		if (schedulerQueue.Contains(task))
		{
			return false;
		}
		lock (schedulerQueue)
		{
			schedulerQueue.Enqueue(task);
		}
		return true;
	}
}
