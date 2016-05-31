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
        /// Enqueues a task at the correct index based on <see cref="Task.NextOccurrence"/>.
        /// </summary>
        public void Enqueue(Task task)
        {
            var taskCount = _tasks.Count;

            for (int i = 0; i < taskCount; i++)
            {
                var t = ((Task)_tasks[i]);
                if (task.NextOccurrence <= t.NextOccurrence)
                {
                    _tasks.Insert(i, task);
                    return;
                }
            }

            _tasks.Add(task);
        }

        /// <summary>
        /// Dequeues a task at the head of the queue if it is time to run it.
        /// </summary>
        /// <returns>Next task to run if <see cref="NextOccurrence"/> &lt;= <see cref="Utility.GetMachineTime().Ticks"/>; otherwise null </returns>
        public Task Dequeue()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                var task = (Task)_tasks[i];
                if (IsTaskCategoryRunning(task))
                {
                    // skip over any tasks with a category matching the running task list
                    continue;
                }
                if (task.NextOccurrence <= Utility.GetMachineTime().Ticks)
                {
                    _tasks.RemoveAt(i);
                    _runningTasks.Add(task);
                    return task;
                }
            }

            ClearRunningTasks();

            return null;
        }

        private void ClearRunningTasks()
        {
            for (int i = _runningTasks.Count - 1; i >= 0; i--)
            {
                var task = (Task)_runningTasks[i];
                if (task.Complete <= Utility.GetMachineTime().Ticks)
                {
                    task.OnComplete();
                    _runningTasks.RemoveAt(i);
                }
            }
        }

        private bool IsTaskCategoryRunning(Task task)
        {
            for (int i = 0; i < _runningTasks.Count; i++)
            {
                var t = (Task)_runningTasks[i];
                if (t.Category == task.Category)
                {
                    return true;
                }
            }
            return false;
        }
        
        private readonly ArrayList _runningTasks = new ArrayList();
        private readonly ArrayList _tasks = new ArrayList();
    }
}
