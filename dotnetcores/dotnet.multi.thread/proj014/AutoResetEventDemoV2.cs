namespace proj014
{
    /// <summary>
    /// Demo AutoResetEvent using 2 times WaitOne() and call Set() 2 times
    /// </summary>
    internal class AutoResetEventDemoV2
    {
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        public static void Run()
        {
            Thread newThread = new Thread(SomeMethod)
            {
                Name = "NewThread"
            };
            newThread.Start(); //It will invoke the SomeMethod in a different thread
            //To See how the SomeMethod goes in halt state let sleep the main thread for 3 secs
            Thread.Sleep(3000);
            Console.WriteLine("Releasing the WaitOne 1 by Set 1");
            autoResetEvent.Set(); //Set 1 will relase the Wait 1

            //To See how the SomeMethod goes in halt state let sleep the main thread for 5 secs
            // If remove this Set, the NewThread will sleep and stuck at Starting 2
            Thread.Sleep(5000);
            Console.WriteLine("Releasing the WaitOne 2 by Set 2");
            autoResetEvent.Set(); //Set 2 will relase the Wait 2
            Console.ReadKey();
        }

        static void SomeMethod()
        {
            Console.WriteLine("Starting 1........");
            autoResetEvent.WaitOne(); //Wait 1
            Console.WriteLine("Finishing 1........");
            Console.WriteLine();
            Console.WriteLine("Starting 2........");
            autoResetEvent.WaitOne(); //Wait 2
            Console.WriteLine("Finishing 2........");
        }
    }
}
