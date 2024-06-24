namespace proj007
{
    public class SimpleMonitorExampleV2
    {
        private static readonly object lockPrintNumbers = new object();

        public static void PrintNumbers()
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds(1000);
            bool lockTaken = false;
            try
            {
                Console.WriteLine(Thread.CurrentThread.Name + " Trying to enter into the critical section");
                Monitor.TryEnter(lockPrintNumbers, timeout, ref lockTaken);
                if (lockTaken)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " Entered into the critical section");
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(100);
                        Console.Write(i + ",");
                    }
                    Console.WriteLine();
                }
                else
                {
                    // The lock was not acquired.
                    Console.WriteLine(Thread.CurrentThread.Name + " Lock was not acquired");
                }
            }
            finally
            {
                // To Ensure that the lock is released.
                if (lockTaken)
                {
                    Monitor.Exit(lockPrintNumbers);
                    Console.WriteLine(Thread.CurrentThread.Name + " Exit from critical section");
                }
            }
        }

        public static void Run()
        {
            Thread[] Threads = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                Threads[i] = new Thread(PrintNumbers)
                {
                    Name = "Child Thread " + i
                };
            }
            foreach (Thread t in Threads)
            {
                t.Start();
            }
            Console.ReadLine();
        }
    }
}
