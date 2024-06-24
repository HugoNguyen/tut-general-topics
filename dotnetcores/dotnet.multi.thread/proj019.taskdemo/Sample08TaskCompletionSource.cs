namespace proj019
{
    internal class Sample08TaskCompletionSource
    {
        public static void Run()
        {
            Console.WriteLine("Enter a number between 1 and 3");
            string value = Console.ReadLine();
            SomeMethod(value);
            Console.ReadKey();
        }
        static async void SomeMethod(string value)
        {
            var task = EvaluateValue(value);
            Console.WriteLine("EvaluateValue Started");
            try
            {
                Console.WriteLine($"Is Completed: {task.IsCompleted}");
                Console.WriteLine($"Is IsCanceled: {task.IsCanceled}");
                Console.WriteLine($"Is IsFaulted: {task.IsFaulted}");
                var result = await task;
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("EvaluateValue Completed");
        }

        static Task<string> EvaluateValue(string value)
        {
            //Creates an object of TaskCompletionSource with the specified options.
            //RunContinuationsAsynchronously option Forces the task to be executed asynchronously.
            var TCS = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
            if (value == "1")
            {
                //Set the underlying Task into the RanToCompletion state.
                TCS.SetResult($"Task Complete with {value}");
            }
            else if (value == "2")
            {
                //Set the underlying Task into the Canceled state.
                TCS.SetCanceled();
            }
            else
            {
                //Set the underlying Task into the Faulted state and binds it to a specified exception.
                TCS.SetException(new ApplicationException($"Invalid Value : {value}"));
            }
            //Return the task associted with the TaskCompletionSource
            return TCS.Task;
        }
    }
}
