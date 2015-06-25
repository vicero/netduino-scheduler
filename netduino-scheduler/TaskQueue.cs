using System;
using System.Collections;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace netduino_scheduler
{
    /// <summary>
    /// Maintains a sorted queue of <see cref="Task"/>s and ensures type safety
    /// </summary>
    internal class TaskQueue
    {
        /// <summary>
        /// Enqueues a task at the correct index based on <see cref="Task.NextOccurrence"/>
        /// </summary>
        public void Enqueue(Task task)
        {
            var taskCount = _tasks.Count;
            for (int i = 0; i < taskCount; i++)
            {
                if (task.NextOccurrence <= ((Task)_tasks[i]).NextOccurrence)
                {
                    _tasks.Insert(i, task);
                    return;
                }
            }
            _tasks.Add(task);
        }

        /// <summary>
        /// Dequeues a task at the head of the queue if it is time to run it
        /// </summary>
        /// <returns>Next task to run if <see cref="NextOccurrence"/> &lt;= <see cref="Utility.GetMachineTime().Ticks"/>; otherwise null </returns>
        public Task Dequeue()
        {
            var task = (Task)_tasks[0];
            if (task.NextOccurrence <= Utility.GetMachineTime().Ticks)
            {
                _tasks.RemoveAt(0);
                return task;
            }
            return null;
        }

        private readonly ArrayList _tasks = new ArrayList();
    }
}
