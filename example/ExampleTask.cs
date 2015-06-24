using System;
using System.Diagnostics;
using System.Threading;

using Microsoft.SPOT;
using netduino_scheduler;

namespace example
{
    internal sealed class ExampleTask : Task
    {
        private string _name;
        private int _wait;

        public ExampleTask(
            string name,
            long recurrence,
            int wait)
            : base(recurrence)
        {
            _name = name;
            _wait = wait;
        }

        protected override void DoWork()
        {
            Debug.Print(_name + " will now wait for " + _wait + "ms");
            Thread.Sleep(_wait);
        }
    }
}
