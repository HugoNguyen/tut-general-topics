namespace proj014
{
    /// <summary>
    /// Demo ManualResetEvent, one Set() can release all WaitOne()
    /// </summary>
    internal class ManualResetEventDemoV2
    {
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
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
            manualResetEvent.Set(); //Set will release all the WaitOne

            Console.ReadKey();
        }
        static void SomeMethod()
        {
            Console.WriteLine("Starting 1........");
            manualResetEvent.WaitOne(); //Wait 1
            Console.WriteLine("Finishing 1........");
            Console.WriteLine();
            Console.WriteLine("Starting 2........");
            manualResetEvent.WaitOne(); //Wait 2
            Console.WriteLine("Finishing 2........");
        }
    }
}
