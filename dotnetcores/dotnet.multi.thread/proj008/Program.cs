namespace proj008
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Mutex mutex = new Mutex(false, "MutexDemo"))
            {
                //Checking if Other External Thread is Running
                if (!mutex.WaitOne(5000, false))
                {
                    Console.WriteLine("An Instance of the Application is Already Running");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Application Is Running.......");
                Console.ReadKey();
            }
        }
    }
}
