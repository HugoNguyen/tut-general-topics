namespace proj021.paralleldemos
{
    internal class Sample09MaxDegreeOfParallelism
    {
        public static void Run()
        {
            //Getting the Number of Processor count
            int processorCount = Environment.ProcessorCount;
            Console.WriteLine($"Processor Count on this Machine: {processorCount}\n");
            //Limiting the maximum degree of parallelism to processorCount - 1
            var options = new ParallelOptions()
            {
                //You can hard code the value as follows
                //MaxDegreeOfParallelism = 7
                //But better to use the following statement
                MaxDegreeOfParallelism = Environment.ProcessorCount - 1
            };
            Parallel.For(1, 30, options, i =>
            {
                //Thread.Sleep(500);
                var timespan = TimeSpan.FromMilliseconds((new Random().Next(3, 10)) * 1000);
                Thread.Sleep(timespan);
                Console.WriteLine($"Value of i = {i}, Sleep in {timespan.TotalSeconds}, Thread = {Thread.CurrentThread.ManagedThreadId}");
            });

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
