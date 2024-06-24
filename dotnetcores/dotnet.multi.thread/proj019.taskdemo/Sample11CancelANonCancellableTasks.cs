namespace proj019
{
    internal class Sample11CancelANonCancellableTasks
    {
        static CancellationTokenSource cancellationTokenSource;
        public static void Run()
        {
            SomeMethod();
            CancelToken();
            Console.ReadKey();
        }
        public static async void SomeMethod()
        {
            cancellationTokenSource = new CancellationTokenSource();
            try
            {
                var result = await Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    Console.WriteLine("Operation was Successful");
                    return 7;
                }).WithCancellation(cancellationTokenSource.Token);
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }

        public static void CancelToken()
        {
            cancellationTokenSource?.Cancel();
        }
    }

    public static class TaskExtensionMethods
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var TCS = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
            using (cancellationToken.Register(state =>
            {
                ((TaskCompletionSource<object>)state).TrySetResult(null);
            }, TCS))
            {
                var resultTask = await Task.WhenAny(task, TCS.Task);
                if (resultTask == TCS.Task)
                {
                    throw new OperationCanceledException(cancellationToken);
                }
                return await task;
            };
        }
    }
}
