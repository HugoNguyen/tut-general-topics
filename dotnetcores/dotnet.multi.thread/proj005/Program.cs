namespace proj005
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // WithoutJoin();
            // WithJoin();
            // WithJoinV2();
            // WithJoinV3();
            WithIsAlive();
            Console.Read();
        }

        /// <summary>
        /// <para>The Main thread is not waiting for all the child threads to complete their execution or task</para>
        /// </summary>
        static void WithoutJoin()
        {
            Console.WriteLine("Main Thread Started");
            //Main Thread creating three child threads
            Thread thread1 = new Thread(Method1);
            Thread thread2 = new Thread(Method2);
            Thread thread3 = new Thread(Method3);
            thread1.Start();
            thread2.Start();
            thread3.Start();
            Console.WriteLine("Main Thread Ended");
        }

        /// <summary>
        /// <para>the Main thread is started first and will wait until all the child threads complete their execution</para>
        /// </summary>
        static void WithJoin()
        {
            Console.WriteLine("Main Thread Started");
            //Main Thread creating three child threads
            Thread thread1 = new Thread(Method1);
            Thread thread2 = new Thread(Method2);
            Thread thread3 = new Thread(Method3);
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join(); //Block Main Thread until thread1 completes its execution
            thread2.Join(); //Block Main Thread until thread2 completes its execution
            thread3.Join(); //Block Main Thread until thread3 completes its execution
            Console.WriteLine("Main Thread Ended");
        }

        static void WithJoinV2()
        {
            Console.WriteLine("Main Thread Started");
            //Main Thread creating three child threads
            Thread thread1 = new Thread(Method1);
            Thread thread2 = new Thread(Method2);
            Thread thread3 = new Thread(Method3);
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join(); //Block Main Thread until thread1 completes its execution
            thread2.Join(); //Block Main Thread until thread2 completes its execution
            //Now, Main Thread will not wait for thread3 to complete its execution
            Console.WriteLine("Main Thread Ended");
        }

        static void WithJoinV3()
        {
            Console.WriteLine("Main Thread Started");
            //Main Thread creating three child threads
            Thread thread1 = new Thread(Method1);
            Thread thread2 = new Thread(Method2);
            Thread thread3 = new Thread(Method3);
            thread1.Start();
            thread2.Start();
            thread3.Start();

            //Now, Main Thread will block for 3 seconds and wait thread2 to complete its execution
            if (thread2.Join(TimeSpan.FromSeconds(3)))
            {
                Console.WriteLine("Thread 2 Execution Completed in 3 second");
            }
            else
            {
                Console.WriteLine("Thread 2 Execution Not Completed in 3 second");
            }
            //Now, Main Thread will block for 3 seconds and wait thread3 to complete its execution
            if (thread3.Join(3000))
            {
                Console.WriteLine("Thread 3 Execution Completed in 3 second");
            }
            else
            {
                Console.WriteLine("Thread 3 Execution Not Completed in 3 second");
            }
            Console.WriteLine("Main Thread Ended");
        }

        static void WithIsAlive()
        {
            Console.WriteLine("Main Thread Started");
            Thread thread1 = new Thread(Method1);
            thread1.Start();
            if (thread1.IsAlive)
            {
                Console.WriteLine("Thread1 Method1 is still Executing");
            }
            else
            {
                Console.WriteLine("Thread1 Method1 Completed its work");
            }
            //Wait Till thread1 to complete its execution
            thread1.Join();
            if (thread1.IsAlive)
            {
                Console.WriteLine("Thread1 Method1 is still Executing");
            }
            else
            {
                Console.WriteLine("Thread1 Method1 Completed its work");
            }
            Console.WriteLine("Main Thread Ended");
        }

        static void Method1()
        {
            Console.WriteLine("Method1 - Thread1 Started");
            Thread.Sleep(3000);
            Console.WriteLine("Method1 - Thread 1 Ended");
        }
        static void Method2()
        {
            Console.WriteLine("Method2 - Thread2 Started");
            Thread.Sleep(2000);
            Console.WriteLine("Method2 - Thread2 Ended");
        }
        static void Method3()
        {
            Console.WriteLine("Method3 - Thread3 Started");
            Thread.Sleep(5000);
            Console.WriteLine("Method3 - Thread3 Ended");
        }
    }
}
