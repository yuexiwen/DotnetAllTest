namespace CosmosWideTablePoc
{
    internal abstract class SleepThread
    {
        private int SleepTime { get; set; }

        public abstract void Action();

        public void RunTask()
        {
            while (true)
            {
                Action();
                Thread.Sleep(SleepTime);
            }
        }

        public void StartTask(int sleepTime = 10000)
        {
            SleepTime = sleepTime;
            Console.WriteLine("start task");
            Task.Run(() => RunTask());
        }
    }
}
