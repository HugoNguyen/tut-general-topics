namespace proj019
{
    internal class Sample10AttachedChildTasks
    {
        /// <summary>
        /// Output:
        ///     Main Method Started
        ///     Outer Task Started
        ///     Outer Task Completed
        ///     Main Method Completed
        ///     Child Task Started
        ///     Child Task Completed
        /// Explanation:
        ///     Main wait parentTask completed
        ///     ParentTask wait childTask completed (because attached mode)
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("Main Method Started");
            //Creating the Parent Task with DenyChildAttach Option
            var parentTask = Task.Factory.StartNew(() => {
                Console.WriteLine("Outer Task Started");
                //Creating the Child Task with AttachedToParent
                var childTask = Task.Factory.StartNew(() => {
                    Console.WriteLine("Child Task Started");
                    Thread.Sleep(5000);
                    Console.WriteLine("Child Task Completed");
                }, TaskCreationOptions.AttachedToParent);
                Console.WriteLine("Outer Task Completed");
            }, TaskCreationOptions.DenyChildAttach);
            //Waiting for the Parent Task to completed.
            parentTask.Wait();
            Console.WriteLine("Main Method Completed");
            Console.ReadKey();
        }
    }
}
