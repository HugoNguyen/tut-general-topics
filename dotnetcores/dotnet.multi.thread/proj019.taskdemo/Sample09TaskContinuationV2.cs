namespace proj019
{
    internal class Sample09TaskContinuationV2
    {
        public static void Run()
        {
            Console.WriteLine("Enter a number between 1 and 3");
            
            string value = Console.ReadLine();
            var task = Process(value);

            task.ContinueWith((i) =>
            {
                Console.WriteLine("TasK Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled);
            task.ContinueWith((i) =>
            {
                Console.WriteLine("Task Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);
            var completedTask = task.ContinueWith((i) =>
            {
                Console.WriteLine($"Task Completed {i.Result}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            try
            {
                completedTask.Wait();
            }
            catch
            {

            }

            Console.ReadKey();
        }

        static Task<int> Process(string value)
        {
            var taskSource = new TaskCompletionSource<int>();

            var successed = int.TryParse(value, out int  result);

            if (successed == true && result == 1)
            {
                taskSource.SetResult(result);
            }
            else if (successed == true && result == 2)
            {
                taskSource.SetCanceled();
            }
            else
            {
                taskSource.SetException(new ApplicationException($"Invalid Value : {value}"));
            }

            return taskSource.Task;
        }
    }
}
