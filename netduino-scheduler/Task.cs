using System;
using System.Diagnostics;

using Microsoft.SPOT;

namespace netduino_scheduler
{
    /// <summary>
    /// Task that will be scheduled and ran by the scheduler
    /// </summary>
    [DebuggerDisplay("Next Occurrence { NextOccurrence }")]
    public abstract class Task
    {
        public Task(long recurrence)
        {
            var minimumRecurrence = System.TimeSpan.TicksPerMillisecond * 10;
            if (recurrence <= minimumRecurrence)
            {
                throw new Exception("Recurrence must be greater than " + minimumRecurrence + " ticks");
            }
            _recurrence = recurrence;
        }

        /// <summary>
        /// Called by <see cref="Scheduler"/>.
        /// </summary>
        /// <remarks>
        /// Updates <see cref="NextOccurrence"/> after performing <see cref="DoWork"/>
        /// </remarks>
        public void Execute()
        {
            DoWork();
            UpdateNextOccurrence();
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
            _nextOcurrence = Microsoft.SPOT.Hardware.Utility.GetMachineTime().Ticks + _recurrence;
        }

        /// <summary>
        /// Recurrence in ticks
        /// </summary>
        public long Recurrence { get { return _recurrence; } }

        /// <summary>
        /// Next Occurence in ticks
        /// </summary>
        public long NextOccurrence { get { return _nextOcurrence; } }
        
        private long _nextOcurrence;
        private long _recurrence;
    }
}
