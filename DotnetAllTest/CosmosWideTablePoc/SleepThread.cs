namespace CosmosWideTablePoc
{
    internal abstract class SleepThread
    {
        private int SleepTime { get; set; }

        public abstract void Action();

        public void RunTask()
        {
            while(true)
            {
                this.Action();
                Thread.Sleep(this.SleepTime);
            }
        }

        public void StartTask(int sleepTime = 10000)
        {
            this.SleepTime = sleepTime;
            Console.WriteLine("start task");
            Task.Run(() => this.RunTask());
        }
    }
}
