namespace proj010
{
    internal class Sample01
    {
        //only 3 threads can access resource simulteniously
        static SemaphoreSlim semaphore = new SemaphoreSlim(initialCount: 3);

        static void SemaphoreSlimFunction(string name, int seconds)
        {
            Console.WriteLine($"{name} Waits to access resource");
            semaphore.Wait();
            Console.WriteLine($"{name} was granted access to resource");
            Thread.Sleep(seconds);
            Console.WriteLine($"{name} is completed");
            var semaphoreCount = semaphore.Release();
            Console.WriteLine($"{name} releases the semaphore; previous count: {semaphoreCount}");
        }

        public static void Run()
        {
            Console.WriteLine($"{semaphore.CurrentCount} tasks can enter the semaphore");

            for (int i = 1; i <= 5; i++)
            {
                int count = i;
                Thread t = new Thread(() => SemaphoreSlimFunction("Thread " + count, 1000 * count));
                t.Start();
            }
        }
    }
}
