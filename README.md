# netduino-scheduler

A recurrence scheduler in C# for the Netduino.

## Getting Started

You will need to install the Netduino SDK for .NET Micro 4.3 as described [here](http://www.netduino.com/downloads/).  Then include the netduino-scheduler project in your solution.  The example project is not required.

## Tasks

Work done by the system is described by the abstract `Task` class.  To schedule work, first create your own class that inherits from `Task`.  The `recurrence` parameter on the base constructor tells the scheduler how often to perform the task.

```
internal sealed class ExampleTask : Task
{
	public ExampleTask() : base(recurrence: System.TimeSpan.TicksPerMillisecond * 1000) { }
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
        new ExampleTask("A", System.TimeSpan.TicksPerMillisecond*1000, 300),
        new ExampleTask("B", System.TimeSpan.TicksPerMillisecond*1000, 400),
        new ExampleTask("C", System.TimeSpan.TicksPerMillisecond*1000, 500),
    };

    var scheduler = new Scheduler(tasks);
    scheduler.Run();
}
```

## example project debug output

```
C will now wait for 500ms
A will now wait for 300ms
B will now wait for 400ms
C will now wait for 500ms
A will now wait for 300ms
B will now wait for 400ms
C will now wait for 500ms
A will now wait for 300ms
B will now wait for 400ms
C will now wait for 500ms
A will now wait for 300ms
B will now wait for 400ms
C will now wait for 500ms
A will now wait for 300ms
B will now wait for 400ms
C will now wait for 500ms
A will now wait for 300ms
```