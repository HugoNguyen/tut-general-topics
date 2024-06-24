namespace proj019
{
    internal class Sample10DetachedChildTasks
    {
        /// <summary>
        /// Output:
        ///     Main Method Started
        ///     Outer Task Started
        ///     Outer Task Completed
        ///     Child Task Started
        ///     Main Method Completed
        ///     Child Task Completed
        /// Explanation:
        ///     Main only wait parentTask (parentTask.Wait())
        ///     the parentTask is not waiting for the child task to complete its execution.
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("Main Method Started");
            //Creating the Parent Task
            var parentTask = Task.Factory.StartNew(() => {
                Console.WriteLine("Outer Task Started");
                //Creating the Child Task
                var childTask = Task.Factory.StartNew(() => {
                    Console.WriteLine("Child Task Started.");
                    Thread.Sleep(5000);
                    Console.WriteLine("Child Task Completed");
                    return Task.CompletedTask;
                });

                // Parent Task will wait for detached Child Task to complete its execution
                // Uncomment if want ParentTask wait childTask
                // childTask.Wait();
                Console.WriteLine("Outer Task Completed");
            });
            //Waiting for the Parent Task to completed. Not the Child Task
            parentTask.Wait();
            Console.WriteLine("Main Method Completed");
            Console.ReadKey();
        }
    }
}
