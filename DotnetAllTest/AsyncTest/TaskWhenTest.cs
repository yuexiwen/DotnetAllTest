namespace AsyncTest
{
    internal class TaskWhenTest
    {
        public static async Task<string> TestFunction()
        {
            return await Task.Run(() => 
            {
                Console.WriteLine("TestFunction");
                Thread.Sleep(10000);
                return "sldalfasfd";
            });
        }

        public static async Task<string> isTaskExpiration(int expirationTime)
        {
            Task timeoutWaitTask = Task.Delay(expirationTime);
            var task = TestFunction();
            if (task == await Task.WhenAny(task, timeoutWaitTask))
            {
                return await task;
            }

            throw new Exception("time out");
        }
    }
}
