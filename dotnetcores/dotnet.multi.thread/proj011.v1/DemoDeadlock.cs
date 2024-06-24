namespace proj011.v1
{
    internal class DemoDeadlock
    {
        /// <summary>
        /// <para>For thread1, Account1001 is resource1 and Account1002 is resource2. On the other hand, for thread2, Account1002 is resource1, and Account1001 is resource2. Now, run the application and see if a deadlock occurred.</para>
        /// <para>The reason for the deadlock is that thread1 acquired an exclusive lock on Account1001 and then did some processing. In the meantime, thread2 started, and it acquired an exclusive lock on Account1002 and then did some processing. Then thread1 back and wanted to acquire a lock on Account1002 which is already locked by thread2. Similarly, thread2 is back and wants to acquire a lock on Account1001, which is already locked by thread1 and hence deadlock.</para>
        /// </summary>
        public static void TestRunIntoDeadlock()
        {
            Console.WriteLine("Main Thread Started");
            Account Account1001 = new Account(1001, 5000);
            Account Account1002 = new Account(1002, 3000);
            var accountManager1 = new AccountManagerV1(Account1001, Account1002, 5000);
            Thread thread1 = new Thread(accountManager1.FundTransfer)
            {
                Name = "Thread1"
            };
            var accountManager2 = new AccountManagerV1(Account1002, Account1001, 6000);
            Thread thread2 = new Thread(accountManager2.FundTransfer)
            {
                Name = "Thread2"
            };
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("Main Thread Completed");
        }

        public static void TestAvoidDeadlockWithMonitorTryEnter()
        {
            Console.WriteLine("Main Thread Started");
            Account Account1001 = new Account(1001, 5000);
            Account Account1002 = new Account(1002, 3000);
            var accountManager1 = new AccountManagerV2(Account1001, Account1002, 5000);
            Thread thread1 = new Thread(accountManager1.FundTransfer)
            {
                Name = "Thread1"
            };
            var accountManager2 = new AccountManagerV2(Account1002, Account1001, 6000);
            Thread thread2 = new Thread(accountManager2.FundTransfer)
            {
                Name = "Thread2"
            };
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("Main Thread Completed");
        }

        public static void TestAvoidDeadlockByAcquiringLockInSpecificOrder()
        {
            Console.WriteLine("Main Thread Started");
            Account Account1001 = new Account(1001, 5000);
            Account Account1002 = new Account(1002, 3000);
            var accountManager1 = new AccountManagerV3(Account1001, Account1002, 5000);
            Thread thread1 = new Thread(accountManager1.FundTransfer)
            {
                Name = "Thread1"
            };
            var accountManager2 = new AccountManagerV3(Account1002, Account1001, 6000);
            Thread thread2 = new Thread(accountManager2.FundTransfer)
            {
                Name = "Thread2"
            };
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("Main Thread Completed");
        }
    }
}
