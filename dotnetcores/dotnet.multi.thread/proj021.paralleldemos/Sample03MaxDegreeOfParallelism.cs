namespace proj021.paralleldemos
{
    internal class Sample03MaxDegreeOfParallelism
    {
        public static void Run()
        {
            Default();
            WithLimit();

            Console.WriteLine("Press any key to exist");
            Console.ReadLine();
        }

        static void Default()
        {
            Console.WriteLine("Default");
            int n = 10;
            Parallel.For(0, n, i =>
            {
                DoSomeTask(i);
            });
        }

        static void WithLimit()
        {
            Console.WriteLine("WithLimit 2");

            //Limiting the maximum degree of parallelism to 2
            //If MaxDegreeOfParallelism is -1, there is no limit on the number of concurrently running operations.
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 2
            };
            int n = 10;
            Parallel.For(0, n, options, i =>
            {
                DoSomeTask(i);
            });
        }

        static void DoSomeTask(int number)
        {
            Console.WriteLine($"DoSomeTask {number} started by Thread {Thread.CurrentThread.ManagedThreadId}");
            //Sleep for 5000 milliseconds
            Thread.Sleep(5000);
            Console.WriteLine($"DoSomeTask {number} completed by Thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
