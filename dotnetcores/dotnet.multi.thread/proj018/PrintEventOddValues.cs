﻿namespace proj018
{
    /// <summary>
    /// print the Even and Odd number sequence using 2 different threads
    /// <para>Thread T1: 0,2,4,6,8…</para>
    /// <para>Thread T2:1,3,5,7,9…</para>
    /// <para>Output: 0,1,2,3,4,5,6,7,8,9…</para>
    /// </summary>
    internal class PrintEventOddValues
    {
        //Limit numbers will be printed on the Console
        const int numberLimit = 10;
        static readonly object _lockObject = new object();
        public static void Run()
        {
            Thread EvenThread = new Thread(PrintEvenNumbers);
            Thread OddThread = new Thread(PrintOddNumbers);
            //First Start the Even thread.
            EvenThread.Start();
            //Pause for 10 ms, to make sure Even thread has started 
            //or else Odd thread may start first resulting different sequence.
            Thread.Sleep(100);
            //Next, Start the Odd thread.
            OddThread.Start();
            //Wait for all the childs threads to complete
            OddThread.Join();
            EvenThread.Join();
            Console.ReadKey();
        }
        //Printing of Even Numbers Function
        static void PrintEvenNumbers()
        {
            try
            {
                //Implement lock as the Console is shared between two threads
                Monitor.Enter(_lockObject);
                for (int i = 0; i <= numberLimit; i = i + 2)
                {
                    //Printing Even Number on Console)
                    Console.Write($"{i} ");
                    //Notify Odd thread that I'm done, you do your job
                    Monitor.Pulse(_lockObject);
                    //I will wait here till Odd thread notify me 
                    // Monitor.Wait(monitor);
                    //Without this logic application will wait forever
                    bool isLast = false;
                    if (i == numberLimit)
                    {
                        isLast = true;
                    }
                    if (!isLast)
                    {
                        //I will wait here till Odd thread notify me
                        Monitor.Wait(_lockObject);
                    }
                }
            }
            finally
            {
                //Release the lock
                Monitor.Exit(_lockObject);
            }
        }
        //Printing of Odd Numbers Function
        static void PrintOddNumbers()
        {
            try
            {
                //Hold lock as the Console is shared between two threads
                Monitor.Enter(_lockObject);
                for (int i = 1; i <= numberLimit; i = i + 2)
                {
                    //Printing the odd numbers on the console
                    Console.Write($"{i} ");
                    //Notify Even thread that I'm done, you do your job
                    Monitor.Pulse(_lockObject);
                    //I will wait here till even thread notify me
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
                        Monitor.Wait(_lockObject);
                    }
                }
            }
            finally
            {
                //Release lock
                Monitor.Exit(_lockObject);
            }
        }
    }
}
