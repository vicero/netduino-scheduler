using System;
using System.Threading;

using Microsoft.SPOT;

namespace netduino_scheduler
{
    /// <summary>
    /// Scheduler implementation
    /// </summary>
    public sealed class Scheduler
    {
        /// <summary/>
        /// <param name="schedulableTasks">Tasks that will be scheduled and rescheduled</param>
        /// <param name="granularityInMilliseconds"></param>
        public Scheduler(
            Task[] schedulableTasks,
            int granularityInMilliseconds = 1000)
        {
            _schedulableTasks = schedulableTasks;
            _granularityInMilliseconds = granularityInMilliseconds;

            for (int i = 0; i < _schedulableTasks.Length; i++)
            {
                _taskQueue.Enqueue(_schedulableTasks[i]);
            }
        }

        /// <summary>
        /// Main loop entry point
        /// </summary>
        public void Run()
        {
            while (true)
            {
                var task = _taskQueue.Dequeue();
                if (task != null)
                {
                    RunTask(task);
                }
                Thread.Sleep(_granularityInMilliseconds);
            }
        }

        private void RunTask(Task task)
        {
            task.Execute();
            _taskQueue.Enqueue(task);
        }

        private readonly TaskQueue _taskQueue = new TaskQueue();
        private readonly Task[] _schedulableTasks;
        private readonly int _granularityInMilliseconds;
    }
}
