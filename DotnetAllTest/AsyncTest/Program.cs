
using AsyncTest;
/*
var task = SimplestTask.run(() =>
{
    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(1000);
        Console.WriteLine(i);
    }
});

Console.WriteLine("hello");
task.wait();
Console.WriteLine("world");
*/

var task = MyTask.Run(() =>
{
    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(1000);
        Console.WriteLine(i);
    }
});
Console.WriteLine("hello");
task.wait();
Console.WriteLine("world");