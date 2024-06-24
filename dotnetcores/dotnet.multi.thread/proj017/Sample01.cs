namespace proj017
{
    internal class Sample01
    {
        public static void Run()
        {
            // Creating and initializing threads
            Thread thread = new Thread(SomeMethod);
            thread.Start();
            Console.WriteLine("Thread is Abort");
            // Abort thread Using Abort() method
            thread.Abort();
            Console.ReadKey();
        }

        public static void SomeMethod()
        {
            for (int x = 0; x < 3; x++)
            {
                Console.WriteLine(x);
            }
        }
    }
}
