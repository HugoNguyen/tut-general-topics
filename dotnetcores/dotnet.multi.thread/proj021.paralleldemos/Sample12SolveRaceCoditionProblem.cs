using System.Diagnostics;

namespace proj021.paralleldemos
{
    internal class Sample12SolveRaceCoditionProblem
    {
        public static void Run()
        {
            WithoutInterlocked();
            WithInterlocked();
            WithLock();
            Console.ReadKey();
        }

        static void WithoutInterlocked()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("WithoutInterlocked Begin");
            var ValueWithoutInterlocked = 0;
            Parallel.For(0, 100000, _ =>
            {
                //Incrementing the value
                ValueWithoutInterlocked++;
            });
            Console.WriteLine("Expected Result: 100000");
            Console.WriteLine($"Actual Result: {ValueWithoutInterlocked}");
            stopwatch.Stop();
            Console.WriteLine($"WithoutInterlocked End Execution Time {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
        }

        static void WithInterlocked()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("WithInterlocked Begin");
            var ValueInterlocked = 0;
            Parallel.For(0, 100000, _ =>
            {
                //Incrementing the value
                Interlocked.Increment(ref ValueInterlocked);
            });
            Console.WriteLine("Expected Result: 100000");
            Console.WriteLine($"Actual Result: {ValueInterlocked}");
            stopwatch.Stop();
            Console.WriteLine($"WithInterlocked End Execution Time {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
        }

        static object lockObject = new object();
        static void WithLock()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("WithLock Begin");
            var ValueWithLock = 0;
            Parallel.For(0, 100000, _ =>
            {
                lock (lockObject)
                {
                    //Incrementing the value
                    ValueWithLock++;
                }
            });
            Console.WriteLine("Expected Result: 100000");
            Console.WriteLine($"Actual Result: {ValueWithLock}");
            stopwatch.Stop();
            Console.WriteLine($"WithLock End Execution Time {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
        }
    }
}
