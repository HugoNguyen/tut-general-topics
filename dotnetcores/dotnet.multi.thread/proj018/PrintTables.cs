namespace proj018
{
    /// <summary>
    /// Print table 5 and 4 in 2 thread. Without using Wait and Push.
    /// </summary>
    internal class PrintTables
    {
        static readonly object _lockObject = new object();
        public static void Run()
        {
            //Creating an object of Thread class to Execute the PrintTable method
            Thread thread = new Thread(PrintTable)
            {
                Name = "Manual Thread"
            };
            thread.Start();
            //Locking the _lockObject
            lock (_lockObject)
            {
                Thread th = Thread.CurrentThread;
                th.Name = "Main Thread";
                Console.WriteLine($"{th.Name} Running and Printing the Table of 5");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("5 x " + i + " = " + (5 * i));
                }
            } //synchronized block ends
            Console.ReadKey();
        }

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
            }
        }
    }
}
