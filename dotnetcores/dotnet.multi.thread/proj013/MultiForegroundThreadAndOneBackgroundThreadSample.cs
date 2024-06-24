namespace proj013
{
    internal class MultiForegroundThreadAndOneBackgroundThreadSample
    {
        public static void Run()
        {
            // A thread created here to run Method1 Parallely
            Thread thread1 = new Thread(Method1)
            {
            };
            Console.WriteLine($"Thread1 is a Background thread:  {thread1.IsBackground}");
            thread1.Start();
            //The control will come here and will exit 
            //the main thread or main application
            Console.WriteLine("Main Thread Exited");
            //As the Main thread (i.e. foreground thread exits the application)
            //Automatically, the background thread quits the application
        }

        // Static method
        static void Method1()
        {
            Console.WriteLine("Method1 Started");
            Thread thread2 = new Thread(Method2)
            {
                IsBackground = true
            };
            thread2.Start();
            Thread.Sleep(3000);
            Console.WriteLine("Method1 Exited");
        }
        // Static method
        static void Method2()
        {
            Console.WriteLine("Method2 Started");
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine("Method2 is in Progress!!");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Method2 Exited");
            Console.WriteLine("Press any key to Exit.");
            Console.ReadKey();
        }
    }
}
