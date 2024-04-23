namespace AsyncTest
{
    internal class TaskAsyncTest
    {
        public static async Task IterAsync1()
        {
            for (int i = 0; i < 10; ++i)
            {
                await TestFunc1Async();
            }
        }

        public static async Task IterAsync2()
        {
            for (int i = 0; i < 10; ++i)
            {
                await TestFunc2Async();
            }
        }

        public static void IterAsync3()
        {
            for (int i = 0; i < 10; ++i)
            {
                _ = TestFunc3Async();
            }
        }

        public static async Task<string> TestFunc1Async()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine("TestFunc1Async");
                return "11111111";
            });
        }

        public static async Task<string> TestFunc2Async()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(7000);
                Console.WriteLine("TestFunc2Async");
                return "22222222";
            });
        }

        public static async Task<string> TestFunc3Async()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("TestFunc3Async");
                return "33333333";
            }); ;
        }
    }
}
