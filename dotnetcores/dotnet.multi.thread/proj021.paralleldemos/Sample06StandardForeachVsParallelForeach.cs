using System.Diagnostics;

namespace proj021.paralleldemos
{
    internal class Sample06StandardForeachVsParallelForeach
    {
        public static void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Standard Foreach Loop Started");
            stopwatch.Start();
            List<int> integerList = Enumerable.Range(1, 10).ToList();
            foreach (int i in integerList)
            {
                DoSomeIndependentTask(i);
            };

            stopwatch.Stop();
            Console.WriteLine("Standard Foreach Loop Ended");
            Console.WriteLine($"Time Taken by Standard Foreach Loop in Miliseconds {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine("\nParallel Foreach Loop Started");
            stopwatch.Restart();

            Parallel.ForEach(integerList, i =>
            {
                DoSomeIndependentTask(i);
            });

            stopwatch.Stop();
            Console.WriteLine("Parallel Foreach Loop Ended");
            Console.WriteLine($"Time Taken by Parallel Foreach Loop in Miliseconds {stopwatch.ElapsedMilliseconds}");

            Console.ReadLine();
        }
        static void DoSomeIndependentTask(int i)
        {
            Console.WriteLine($"Number: {i}");
        }
    }
}
