namespace proj011.v1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // DemoDeadlock.TestRunIntoDeadlock();
            DemoDeadlock.TestAvoidDeadlockWithMonitorTryEnter();
            //DemoDeadlock.TestAvoidDeadlockByAcquiringLockInSpecificOrder();
            Console.ReadKey();
        }
    }
}
