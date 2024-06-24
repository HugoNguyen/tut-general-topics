namespace proj014
{
    internal class ManualResetEventDemo
    {
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        public static void Run()
        {
            Thread newThread = new Thread(SomeMethod)
            {
                Name = "NewThread"
            };
            newThread.Start(); //It will invoke the SomeMethod in a different thread
            //To See how the SomeMethod goes in halt mode
            //Once we enter any key it will call set method and the SomeMethod will Resume its work
            Console.ReadLine();
            //It will send a signal to other threads to resume their work
            manualResetEvent.Set();
        }
        static void SomeMethod()
        {
            Console.WriteLine("Starting........");
            //Put the current thread into waiting state until it receives the signal
            manualResetEvent.WaitOne(); //It will make the thread in halt mode
            Console.WriteLine("Finishing........");
            Console.ReadLine(); //To see the output in the console
        }
    }
}
