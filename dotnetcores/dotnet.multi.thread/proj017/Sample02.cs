namespace proj017
{
    internal class Sample02
    {
        public static void Run()
        {
            Thread thread = new Thread(SomeMethod)
            {
                Name = "Thread 1"
            };
            thread.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Abort Thread Thread 1");
            thread.Abort(100);
            // Waiting for the thread to terminate.
            thread.Join();
            Console.WriteLine("Main thread is terminating");
            Console.ReadKey();
        }
        public static void SomeMethod()
        {
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is Starting");
                for (int j = 1; j <= 100; j++)
                {
                    Console.Write(j + " ");
                    if ((j % 10) == 0)
                    {
                        Console.WriteLine();
                        Thread.Sleep(200);
                    }
                }
                Console.WriteLine($"{Thread.CurrentThread.Name} Exiting Normally");
            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is aborted and the code is {ex.ExceptionState}");
            }
        }
    }
}
