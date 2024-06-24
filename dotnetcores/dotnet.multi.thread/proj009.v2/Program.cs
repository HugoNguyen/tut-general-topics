namespace proj009.v2
{
    internal class Program
    {
        // maximum of two threads are entering the critical section and doing their tasks at any given point in time.
        public static Semaphore semaphore = new Semaphore(2, 3);
        static void Main(string[] args)
        {
            for (int i = 1; i <= 10; i++)
            {
                Thread threadObject = new Thread(DoSomeTask)
                {
                    Name = "Thread " + i
                };
                threadObject.Start();
            }
            Console.ReadKey();
        }
        static void DoSomeTask()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Wants to Enter into Critical Section for processing");
            try
            {
                //Blocks the current thread until the current WaitHandle receives a signal.   
                semaphore.WaitOne();
                //Decrease the Initial Count Variable by 1
                Console.WriteLine("Success: " + Thread.CurrentThread.Name + " is Doing its work");
                Thread.Sleep(5000);
                Console.WriteLine(Thread.CurrentThread.Name + "Exit.");
            }
            finally
            {
                //Release() method to release semaphore  
                //Increase the Initial Count Variable by 1
                semaphore.Release();
            }
        }
    }
}
