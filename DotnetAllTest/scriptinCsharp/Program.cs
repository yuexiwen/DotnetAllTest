using System.Diagnostics;
using System.Management.Automation;

var scriptArguments = "-ExecutionPolicy Bypass -File \"C:\\code\\ps1\\test2.ps1\"";
var processStartInfo = new ProcessStartInfo("powershell.exe", scriptArguments)
{
    RedirectStandardOutput = true,
    RedirectStandardError = true
};
using var process = new Process();
process.StartInfo = processStartInfo;
process.Start();
Console.WriteLine("testtest");

new Thread(() =>
{
    string output = process.StandardOutput.ReadToEnd();
    string error = process.StandardError.ReadToEnd();
    Console.WriteLine(output);
}).Start();

for (int i = 0; i < 100; i++)
{
    Console.WriteLine(i);
    Thread.Sleep(1000);
}

