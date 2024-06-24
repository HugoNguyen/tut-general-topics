namespace proj016
{
    internal class Program
    {
        /// <summary>
        /// Seems ThreadPriority no longer working
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(SomeMethod)
            {
                Name = "Thread 1"
            };
            //Setting the thread Priority as Normal
            thread1.Priority = ThreadPriority.Normal;
            Thread thread2 = new Thread(SomeMethod)
            {
                Name = "Thread 2"
            };
            //Setting the thread Priority as Lowest
            thread2.Priority = ThreadPriority.Lowest;
            Thread thread3 = new Thread(SomeMethod)
            {
                Name = "Thread 3"
            };
            //Setting the thread Priority as Highest
            thread3.Priority = ThreadPriority.Highest;
            //Getting the thread Prioroty
            Console.WriteLine($"Thread 1 Priority: {thread1.Priority}");
            Console.WriteLine($"Thread 2 Priority: {thread2.Priority}");
            Console.WriteLine($"Thread 3 Priority: {thread3.Priority}");
            thread1.Start();
            thread2.Start();
            thread3.Start();
            Console.ReadKey();
        }
        public static void SomeMethod()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name} Printing {i}");
            }
        }
    }
}
