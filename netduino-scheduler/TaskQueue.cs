using System;
using System.Collections;

using Microsoft.SPOT;

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
            if (_tasks.Count == 0)
            {
                _tasks.Add(task);
                return;
            }

            for (int i = 0; i < _tasks.Count; i++)
            {
                if (i == _tasks.Count - 1)
                {
                    _tasks.Add(task);
                    break;
                }
                else if (task.NextOccurrence <= ((Task)_tasks[i]).NextOccurrence)
                {
                    _tasks.Insert(i, task);
                    break;
                }
            }
        }

        /// <summary>
        /// Dequeues a task at the next index
        /// </summary>
        /// <returns></returns>
        public Task Dequeue()
        {
            var task = (Task)_tasks[0];
            _tasks.RemoveAt(0);
            return task;
        }

        private readonly ArrayList _tasks = new ArrayList();
    }
}
