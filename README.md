# netduino-scheduler

A recurrence scheduler in C# for the Netduino.

## Getting Started

You will need to install the Netduino SDK for .NET Micro 4.3 as described [here](http://www.netduino.com/downloads/).  Then include the netduino-scheduler project in your solution.  The example project is not required.

## Tasks

Work done by the system is described by the abstract `Task` class.  To schedule work, first create your own class that inherits from `Task`.  The `recurrence` parameter on the base constructor tells the scheduler how often to perform the task.

```
internal sealed class ExampleTask : Task
{
	public ExampleTask() : base(recurrence: 1000) { }
}
```

The work to perform in the task should be implemented in the `DoWork` override.  The task can perform thread waits if needed.

```
protected override void DoWork()
{
    //do something interesting
}
```

Tasks will automatically be requeued after completion.

## Scheduler

To start running the tasks, create an instance of `Scheduler` specifying the list of tasks to queue and run.

Then call `Run()` and the scheduler will handle running all the tasks for you.

```
public static void Main()
{
    var tasks = new[] {
        new ExampleTask("A", recurrence: 1000, wait: 100),
        new ExampleTask("B", recurrence: 5000, wait: 100),
        new ExampleTask("C", recurrence: 10000, wait: 100),
    };

    var scheduler = new Scheduler(tasks);
    scheduler.Run();
}
```

## example project debug output

```
00:09.469: A will now wait for 100ms
00:09.575: B will now wait for 100ms
00:09.677: C will now wait for 100ms
00:10.780: A will now wait for 100ms
00:11.883: A will now wait for 100ms
00:12.986: A will now wait for 100ms
00:14.089: A will now wait for 100ms
00:14.691: B will now wait for 100ms
00:15.294: A will now wait for 100ms
00:16.397: A will now wait for 100ms
00:17.499: A will now wait for 100ms
00:18.602: A will now wait for 100ms
00:19.705: A will now wait for 100ms
00:19.807: C will now wait for 100ms
00:19.910: B will now wait for 100ms
00:21.013: A will now wait for 100ms
00:22.116: A will now wait for 100ms
00:23.218: A will now wait for 100ms
00:24.321: A will now wait for 100ms
00:24.924: B will now wait for 100ms
```