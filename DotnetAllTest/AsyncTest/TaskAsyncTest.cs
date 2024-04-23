namespace AsyncTest
{
    internal class TaskAsyncTest
    {
        public static async Task<string> TestFunc1Async()
        {
            Task<string> result = Task.Run(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine("TestFunc1Async");
                return "11111111";
            });

            return await result;
        }

        public static async Task<string> TestFunc2Async()
        {
            Task<string> result = Task.Run(() =>
            {
                Thread.Sleep(7000);
                Console.WriteLine("TestFunc2Async");
                return "22222222";
            });

            return await result;
        }

        public static async Task<string> TestFunc3Async()
        {
            Task<string> result = Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("TestFunc3Async");
                return "33333333";
            });

            return await result;
        }
    }
}
