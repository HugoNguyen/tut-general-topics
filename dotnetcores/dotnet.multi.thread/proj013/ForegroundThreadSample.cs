namespace proj013
{
    internal class ForegroundThreadSample
    {
        public static void Run()
        {
            // A thread created here to run Method1 Parallely
            Thread thread1 = new Thread(Method1);
            Console.WriteLine($"Thread1 is a Background thread:  {thread1.IsBackground}");
            thread1.Start();
            //The control will come here and will exit 
            //the main thread or main application
            Console.WriteLine("Main Thread Exited");
        }

        // Static method
        static void Method1()
        {
            Console.WriteLine("Method1 Started");
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine("Method1 is in Progress!!");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Method1 Exited");
            Console.WriteLine("Press any key to Exit.");
            Console.ReadKey();
        }
    }
}
