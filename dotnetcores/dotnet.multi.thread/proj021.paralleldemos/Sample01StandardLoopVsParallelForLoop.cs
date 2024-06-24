namespace proj021.paralleldemos
{
    internal class Sample01StandardLoopVsParallelForLoop
    {
        /// <summary>
        /// Shows the difference between standard C# vs Parallel For
        ///     using thread
        ///     the order of the iteration
        /// </summary>
        /*
            C# For Loop
            value of count = 0, thread = 1
            value of count = 1, thread = 1
            value of count = 2, thread = 1
            value of count = 3, thread = 1
            value of count = 4, thread = 1
            value of count = 5, thread = 1
            value of count = 6, thread = 1
            value of count = 7, thread = 1
            value of count = 8, thread = 1
            value of count = 9, thread = 1

            Parallel For Loop
            value of count = 0, thread = 11
            value of count = 5, thread = 12
            value of count = 2, thread = 1
            value of count = 1, thread = 5
            value of count = 4, thread = 7
            value of count = 3, thread = 10
            value of count = 6, thread = 13
            value of count = 7, thread = 14
            value of count = 8, thread = 15
            value of count = 9, thread = 16
         */
        public static void Run()
        {
            Console.WriteLine("C# For Loop");
            int number = 10;
            for (int count = 0; count < number; count++)
            {
                //Thread.CurrentThread.ManagedThreadId returns an integer that 
                //represents a unique identifier for the current managed thread.
                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                //Sleep the loop for 10 miliseconds
                Thread.Sleep(10);
            }
            Console.WriteLine();
            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, number, count =>
            {
                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                //Sleep the loop for 10 miliseconds
                Thread.Sleep(10);
            });
            Console.ReadLine();
        }
    }
}
