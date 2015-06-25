using System;
using System.Diagnostics;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace netduino_scheduler
{
    /// <summary>
    /// Task that will be scheduled and ran by the scheduler
    /// </summary>
    [DebuggerDisplay("Next Occurrence { NextOccurrence }")]
    public abstract class Task
    {
        public Task(int recurrence)
        {
            //1ms minimum recurrence
            var minimumRecurrence = 1;
            if (recurrence <= minimumRecurrence)
            {
                throw new Exception("Recurrence must be greater than " + minimumRecurrence + " ms");
            }
            _recurrence = recurrence * System.TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// Called by <see cref="Scheduler"/>.
        /// </summary>
        /// <remarks>
        /// Updates <see cref="NextOccurrence"/> then performs <see cref="DoWork"/>
        /// </remarks>
        public void Execute()
        {
            UpdateNextOccurrence();
            DoWork();
        }

        /// <summary>
        /// The work to complete.
        /// </summary>
        protected abstract void DoWork();

        /// <summary>
        /// Sets the <see cref="NextOccurrence"/> to <see cref="DateTime.Now.Ticks"/> + <see cref="Recurrence"/>.
        /// </summary>
        private void UpdateNextOccurrence()
        {
            _nextOcurrence = Utility.GetMachineTime().Ticks + _recurrence;
        }

        /// <summary>
        /// Next Occurence in ticks
        /// </summary>
        public long NextOccurrence { get { return _nextOcurrence; } }
        
        private long _nextOcurrence;
        private long _recurrence;
    }
}
