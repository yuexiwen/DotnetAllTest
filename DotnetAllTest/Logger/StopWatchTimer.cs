using System.Runtime.InteropServices;

namespace Logger
{
    public static class StopWatchTimer
    {
        public static long queryPerformanceFrequency = 0;

        static StopWatchTimer()
        {
            QueryPerformanceFrequency(out queryPerformanceFrequency);
        }

        public static long GetCurrent()
        {
            QueryPerformanceCounter(out long sysClock);
            return sysClock;
        }

        public static long ElapsedMilliseconds(long fromSysClock)
        {
            return ElapsedMilliseconds(fromSysClock, GetCurrent());
        }

        public static long ElapsedMilliseconds(long fromSysClock, long toSysClock)
        {
            return (toSysClock - fromSysClock) * 1000 / queryPerformanceFrequency;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("kernel32.dll", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern bool QueryPerformanceFrequency(out long frequency);
    }
}
