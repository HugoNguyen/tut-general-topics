namespace proj007
{
    public class MonitorWaitPulseExample
    {
        //Upto the limit numbers will be printed on the Console
        const int numberLimit = 20;
        static readonly object _lockMonitor = new object();

        //Printing of Even Numbers Function
        static void PrintEvenNumbers()
        {
            try
            {
                //Implement lock as the Console is shared between two threads
                Monitor.Enter(_lockMonitor);
                for (int i = 0; i <= numberLimit; i = i + 2)
                {
                    //Printing Even Number on Console)
                    Console.Write($"{i} ");
                    //Notify Odd thread that I'm done, you do your job
                    //It notifies a thread in the waiting queue of a change in the 
                    //locked object's state.
                    Monitor.Pulse(_lockMonitor);
                    //I will wait here till Odd thread notify me 
                    //Monitor.Wait(monitor);
                    //Without this logic application will wait forever
                    bool isLast = false;
                    if (i == numberLimit)
                    {
                        isLast = true;
                    }
                    if (!isLast)
                    {
                        //I will wait here till Odd thread notify me
                        //Releases the lock on an object and blocks the current thread 
                        //until it reacquires the lock.
                        Monitor.Wait(_lockMonitor);
                    }
                }
            }
            finally
            {
                //Release the lock
                Monitor.Exit(_lockMonitor);
            }
        }
        //Printing of Odd Numbers Function
        static void PrintOddNumbers()
        {
            try
            {
                //Hold lock as the Console is shared between two threads
                Monitor.Enter(_lockMonitor);
                for (int i = 1; i <= numberLimit; i = i + 2)
                {
                    //Printing the odd numbers on the console
                    Console.Write($"{i} ");
                    //Notify Even thread that I'm done, you do your job
                    Monitor.Pulse(_lockMonitor);
                    // I will wait here till even thread notify me
                    // Monitor.Wait(monitor);
                    // without this logic application will wait forever
                    bool isLast = false;
                    if (i == numberLimit - 1)
                    {
                        isLast = true;
                    }
                    if (!isLast)
                    {
                        //I will wait here till Even thread notify me
                        Monitor.Wait(_lockMonitor);
                    }
                }
            }
            finally
            {
                //Release lock
                Monitor.Exit(_lockMonitor);
            }
        }

        public static void Run()
        {
            Thread EvenThread = new Thread(PrintEvenNumbers);
            Thread OddThread = new Thread(PrintOddNumbers);
            //First Start the Even thread.
            EvenThread.Start();
            //Puase for 10 ms, to make sure Even thread has started 
            //or else Odd thread may start first resulting different sequence.
            Thread.Sleep(100);
            //Next, Start the Odd thread.
            OddThread.Start();
            //Wait for all the childs threads to complete
            OddThread.Join();
            EvenThread.Join();
            Console.WriteLine("\nMain method completed");
            Console.ReadKey();
        }
    }
}
