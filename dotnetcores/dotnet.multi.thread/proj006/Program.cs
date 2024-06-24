namespace proj006
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // WithoutLock();
            WithLock();
            Console.ReadKey();
        }

        #region Without Lock

        static void SomeMethodWithoutLock()
        {
            Console.Write("[Welcome To The ");
            Thread.Sleep(1000);
            Console.WriteLine("World of Dotnet!]");
        }

        /// <summary>
        /// Got an unexpected result
        /// </summary>
        static void WithoutLock()
        {
            Thread thread1 = new Thread(SomeMethodWithoutLock)
            {
                Name = "Thread 1"
            };
            Thread thread2 = new Thread(SomeMethodWithoutLock)
            {
                Name = "Thread 2"
            };
            Thread thread3 = new Thread(SomeMethodWithoutLock)
            {
                Name = "Thread 3"
            };
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        #endregion

        #region With Lock

        /// <summary>
        /// Avoid using the same lock object instance for different shared resources, as it might result in a deadlock.
        /// </summary>
        static object lockObject = new object();

        static void SomeMethodWithLock()
        {
            // Locking the Shared Resource for Thread Synchronization
            lock (lockObject)
            {
                Console.Write("[Welcome To The ");
                Thread.Sleep(1000);
                Console.WriteLine("World of Dotnet!]");
            }
        }

        /// <summary>
        /// Got an unexpected result
        /// </summary>
        static void WithLock()
        {
            Thread thread1 = new Thread(SomeMethodWithLock)
            {
                Name = "Thread 1"
            };
            Thread thread2 = new Thread(SomeMethodWithLock)
            {
                Name = "Thread 2"
            };
            Thread thread3 = new Thread(SomeMethodWithLock)
            {
                Name = "Thread 3"
            };
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        #endregion
    }
}
