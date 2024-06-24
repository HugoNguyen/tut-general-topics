using System.Diagnostics;

namespace proj021.paralleldemos
{
    internal class Sample10CancelAParallelInvoke
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
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //Passing ParallelOptions as the first parameter
                Parallel.Invoke(
                        parallelOptions,
                        () => DoSomeTask(1),
                        () => DoSomeTask(2),
                        () => DoSomeTask(3),
                        () => DoSomeTask(4),
                        () => DoSomeTask(5),
                        () => DoSomeTask(6),
                        () => DoSomeTask(7),
                        () => DoSomeTask(8),
                        () => DoSomeTask(9),
                        () => DoSomeTask(10),
                        () => DoSomeTask(11)
                    );
                stopwatch.Stop();
                Console.WriteLine($"Time Taken to Execute all the Methods : {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
            }
            //When the token cancelled, it will throw an exception
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
        static void DoSomeTask(int number)
        {
            Console.WriteLine($"DoSomeTask {number} started by Thread {Thread.CurrentThread.ManagedThreadId}");
            //Sleep for 2 seconds
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine($"DoSomeTask {number} completed by Thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
