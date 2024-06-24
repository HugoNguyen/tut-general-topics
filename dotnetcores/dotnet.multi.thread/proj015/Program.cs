namespace proj015
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Creating and initializing threads Unstarted state
                Thread thread1 = new Thread(SomeMethod);
                Console.WriteLine($"Before Start, IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");
                // Running State
                thread1.Start();
                Console.WriteLine($"After Start(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");
                // thread1 is in suspended state
                thread1.Suspend();
                Console.WriteLine($"After Suspend(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");
                // thread1 is resume to running state
                thread1.Resume();
                Console.WriteLine($"After Resume(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");
                // thread1 is in Abort state
                //In this case, it will start the termination, IsAlive still gives you as true
                thread1.Abort();
                Console.WriteLine($"After Abort(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");
                //Calling the Start Method on a dead thread will result an Exception
                thread1.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }

            Console.ReadKey();
        }
        public static void SomeMethod()
        {
            for (int x = 0; x < 3; x++)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("SomeMethod Completed...");
        }
    }
}
