
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
/*
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
*/

var task1 = TaskAsyncTest.TestFunc1Async();
var task2 = TaskAsyncTest.TestFunc2Async();
var task3 = TaskAsyncTest.TestFunc3Async();
Console.WriteLine("===============");
await task1;
await task2;
Console.WriteLine("------------------------");
await task3;
var value = task1.Result;
Console.WriteLine(value);