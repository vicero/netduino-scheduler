using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using netduino_scheduler;

namespace example
{
    public class Program
    {
        public static void Main()
        {
            var tasks = new[] {
                new ExampleTask("A", recurrence: 1000, wait: 100),
                new ExampleTask("B", recurrence: 5000, wait: 100),
                new ExampleTask("C", recurrence: 10000, wait: 100)
            };

            var scheduler = new Scheduler(tasks, granularityInMilliseconds: 500);
            scheduler.Run();
        }
    }
}
