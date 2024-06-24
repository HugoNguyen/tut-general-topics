namespace proj021.paralleldemos
{
    internal class Sample11CancelAParallelForeach
    {
        public static void Run()
        {
            //Create an Instance of CancellationTokenSource
            var CTS = new CancellationTokenSource();
            //Set when the token is going to cancel the parallel execution
            CTS.CancelAfter(TimeSpan.FromSeconds(5));
            //Create an instance of ParallelOptions class
            var parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 2,
                //Set the CancellationToken value
                CancellationToken = CTS.Token
            };
            try
            {
                List<int> integerList = Enumerable.Range(0, 20).ToList();
                Parallel.ForEach(integerList, parallelOptions, i =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine($"Value of i = {i}, thread = {Thread.CurrentThread.ManagedThreadId}");
                });
            }
            //When the token canceled, it will throw an exception
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Finally dispose the CancellationTokenSource and set its value to null
                CTS.Dispose();
                CTS = null;
            }
            Console.ReadLine();
        }
    }
}
