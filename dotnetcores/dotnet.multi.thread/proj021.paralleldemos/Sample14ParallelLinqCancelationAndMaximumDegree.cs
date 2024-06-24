namespace proj021.paralleldemos
{
    internal class Sample14ParallelLinqCancelationAndMaximumDegree
    {
        public static void Run()
        {
            //Creating an instance of CancellationTokenSource
            var CTS = new CancellationTokenSource();
            //Setting the time when the token is going to cancel the Parallel Operation
            CTS.CancelAfter(TimeSpan.FromMilliseconds(200));
            //Creating a Collection of integer numbers
            var numbers = Enumerable.Range(1, 20);

            //Fetching the List of Even Numbers using PLINQ
            var evenNumbers = numbers
                .AsParallel() //Parallel Processing
                .AsOrdered() //Original Order of the numbers
                .WithDegreeOfParallelism(2) //Maximum of two threads can process the data
                .WithCancellation(CTS.Token) //Cancel the operation after 200 Milliseconds
                .Where(x => x % 2 == 0) //This logic will execute in parallel
                .ToList();
            Console.WriteLine("Even Numbers Between 1 and 20");
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }

            Console.ReadKey();
        }
    }
}
