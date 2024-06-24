namespace proj021.paralleldemos
{
    internal class Sample08ParallelInvokeWithInputAndReturn
    {
        public static void Run()
        {
            int intResult = 0;
            string strResult = string.Empty;
            //Calling Three methods Parallely
            Parallel.Invoke(
                () => intResult = Method1(),
                () => strResult = Method2("Pranaya"),
                () => Method3(100)
            );
            Console.WriteLine($"Method1 Result: {intResult}");
            Console.WriteLine($"Method2 Result: {strResult}");
            Console.WriteLine($"Parallel Execution Completed");
            Console.ReadKey();
        }

        static int Method1()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Method 1 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
            return 100;
        }
        static string Method2(string name)
        {
            Thread.Sleep(3000);
            Console.WriteLine($"Method 2 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
            return "Hello:" + name;
        }
        static void Method3(int number)
        {
            Thread.Sleep(6000);
            Console.WriteLine($"Method 3 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
