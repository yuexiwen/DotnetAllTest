
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
/*
var task4 = TaskAsyncTest.IterAsync1();
Console.WriteLine("now start task4");
var task5 = TaskAsyncTest.IterAsync2();
Console.WriteLine("now start task5");
TaskAsyncTest.IterAsync3();
Console.WriteLine("now start task6");
Console.WriteLine("task 6 complete");
await task5;
Console.WriteLine("task 5 complete");
await task4;
Console.WriteLine("task 4 complete");


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
*/

var task = TaskWhenTest.isTaskExpiration(10001);
var result = await task;
Console.WriteLine(result);