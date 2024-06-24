namespace proj019
{
    /// <summary>
    /// Simple demo Async/Await keywork
    /// </summary>
    internal class Sample01SimpleAsyncAwait
    {
        public static void Run()
        {
            Console.WriteLine($"Thread[{Thread.CurrentThread.ManagedThreadId}] => Main Method Started......");
            SomeMethod();
            Console.WriteLine($"Thread[{Thread.CurrentThread.ManagedThreadId}] => Main Method End");
            Console.ReadKey();
        }
        public async static void SomeMethod()
        {
            Console.WriteLine($"Thread[{Thread.CurrentThread.ManagedThreadId}] => Some Method Started......");
            await Task.Delay(TimeSpan.FromSeconds(10));
            Console.WriteLine("\n");
            Console.WriteLine($"Thread[{Thread.CurrentThread.ManagedThreadId}] => Some Method End");
        }
    }
}
