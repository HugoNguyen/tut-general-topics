using System.Diagnostics;

namespace proj021.paralleldemos
{
    internal class Sample15ParallelLinqAggregate
    {
        public static void Run()
        {
            var random = new Random();
            int[] values = Enumerable.Range(1, 99999999)
                .Select(x => random.Next(1, 1000))
                .ToArray();
            //Min, Max and Average LINQ extension methods
            Console.WriteLine("Min, Max and Average with LINQ");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // var linqStart = DateTime.Now; 
            var linqMin = values.Min();
            var linqMax = values.Max();
            var linqAverage = values.Average();
            stopwatch.Stop();
            var linqTimeMS = stopwatch.ElapsedMilliseconds;
            DisplayResults(linqMin, linqMax, linqAverage, linqTimeMS);
            //Min, Max and Average PLINQ extension methods
            Console.WriteLine("\nMin, Max and Average with PLINQ");
            stopwatch.Restart();
            var plinqMin = values.AsParallel().Min();
            var plinqMax = values.AsParallel().Max();
            var plinqAverage = values.AsParallel().Average();
            stopwatch.Stop();
            var plinqTimeMS = stopwatch.ElapsedMilliseconds;
            DisplayResults(plinqMin, plinqMax, plinqAverage, plinqTimeMS);

            Console.ReadKey();
        }

        static void DisplayResults(int min, int max, double average, double time)
        {
            Console.WriteLine($"Min: {min}\nMax: {max}\n" + $"Average: {average:F}\nTotal time in milliseconds: {time}");
        }
    }
}
