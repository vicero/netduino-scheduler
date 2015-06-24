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
                new ExampleTask("A", tpm*1000, 300),
                new ExampleTask("B", tpm*1000, 400),
                new ExampleTask("C", tpm*1000, 500),
            };

            var scheduler = new Scheduler(tasks);
            scheduler.Run();
        }

        public const Int64 tpm = System.TimeSpan.TicksPerMillisecond;
    }
}
