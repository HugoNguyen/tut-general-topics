namespace proj018
{
    /// <summary>
    /// Print table 5 and 4 in 2 thread. Using Wait and Push.
    /// </summary>
    internal class PrintTablesV2
    {
        static readonly object _lockObject = new object();
        public static void Run()
        {
            //Creating an object ofThread class to Execute the PrintTable method
            Thread thread = new Thread(PrintTable)
            {
                Name = "Manual Thread"
            };
            thread.Start();
            //Locking the _lockObject
            lock (_lockObject)
            {
                //Calling the Wait() method in a synchronized context
                //Doing so, makes the Main Thread stops its execution and wait
                //until it is notified by the Pulse() method
                //on the same object _lockObject
                Monitor.Wait(_lockObject);
                Thread th = Thread.CurrentThread;
                th.Name = "Main Thread";
                Console.WriteLine($"{th.Name} Running and Printing the Table of 5");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("5 x " + i + " = " + (5 * i));
                }
            }
            //synchronized block ends
            Console.ReadKey();
        }

        //The entry-point method of the thread
        public static void PrintTable()
        {
            //Synchronizing or locking the _lockObject 
            //Doing so, restricts any other thread to access a block of code using this _lockObject at the same time.
            lock (_lockObject)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} Running and Printing the Table of 4");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("4 x " + i + " = " + (4 * i));
                }
                //The manually created thread is calling the Pulse() method
                //To notifying the Main thread that it is releasing the lock over the _lockObject
                //And Main Thread could lock the object to continue its work     
                Monitor.Pulse(_lockObject);
            } //synchronized block ends
        }
    }
}
