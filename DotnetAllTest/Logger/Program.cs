// See https://aka.ms/new-console-template for more information
using Logger;
using Logger.DefaultImpl;

//Console.WriteLine("Hello, World!");
//Console.WriteLine($"frequency = {StopWatchTimer.queryPerformanceFrequency}");
//Console.WriteLine($"sysClock = {StopWatchTimer.GetCurrent()}");
//long startTime = StopWatchTimer.GetCurrent();
//Thread.Sleep(997);
//Console.WriteLine($"time = {StopWatchTimer.ElapsedMilliseconds(startTime)}");

var logger = new TraceLogger(new ConsoleLogger());
logger.AddTraceStep("step1");
Thread.Sleep(997);
logger.AddTraceStep("step2");
Thread.Sleep(997);
logger.AddTraceStep("step3");
Thread.Sleep(997);
logger.AddTraceStep("step4");
logger.LogTraceInfo("TestComponent", "TestEvent", new KeyValuePair<string, string>("param1", "value1"), new KeyValuePair<string, string>("param2", "value2"));
