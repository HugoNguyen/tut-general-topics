using System.Diagnostics;

namespace proj021.paralleldemos
{
    internal class Sample07SequentialTaskVsParallelInvoke
    {
        public static void Run()
        {
            SequentialTasks();
            ParallelInvoke();
        }

        static void SequentialTasks()
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            //Calling Three methods sequentially
            Method1();
            Method2();
            Method3();
            stopWatch.Stop();

            Console.WriteLine($"Sequential Execution Took {stopWatch.ElapsedMilliseconds} Milliseconds");
        }

        static void ParallelInvoke()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //Calling Three methods Parallely
            Parallel.Invoke(
                 Method1, Method2, Method3
            );
            stopWatch.Stop();
            Console.WriteLine($"Parallel Execution Took {stopWatch.ElapsedMilliseconds} Milliseconds");
        }

        static void Method1()
        {
            Thread.Sleep(200);
            Console.WriteLine($"Method 1 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
        }
        static void Method2()
        {
            Thread.Sleep(200);
            Console.WriteLine($"Method 2 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
        }
        static void Method3()
        {
            Thread.Sleep(200);
            Console.WriteLine($"Method 3 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
