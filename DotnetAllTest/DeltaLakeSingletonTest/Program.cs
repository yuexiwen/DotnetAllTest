// See https://aka.ms/new-console-template for more information
using DeltaLakeSingletonTest.Utils;

Console.WriteLine("Hello, World!");
var filestatus = FileStatus.Of("path1", 1, 10);
Console.WriteLine($"{filestatus.Path} | {filestatus.Size} | {filestatus.ModificationTime}");