namespace AsyncTest
{
    class SimplestTask
    {
        ManualResetEventSlim mres = new();

        public void wait()
        {
            mres.Wait();
        }

        public void complete()
        {
            Console.WriteLine("1");
            lock (this)
            {
                Console.WriteLine("here!");
                mres.Set();
            }
        }

        public static SimplestTask run(Action action)
        {
            SimplestTask task = new SimplestTask();
            new Thread(() =>
            {
                action();
                task.complete();
            }).Start();

            return task;
        }
    }
}
