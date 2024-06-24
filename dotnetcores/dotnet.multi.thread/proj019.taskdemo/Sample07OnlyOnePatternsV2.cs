using static System.Runtime.InteropServices.JavaScript.JSType;

namespace proj019
{
    internal class Sample07OnlyOnePatternsV2
    {
        public static void Run()
        {
            SomeMethod();
            Console.ReadKey();
        }
        static async void SomeMethod()
        {
            //Calling two Different Method using Generic Only One Pattern
            var content = await GenericOnlyOnePattern(
                  //Calling the HelloMethod
                  (ct) => HelloMethod("Pranaya", ct),
                  //Calling the GoodbyeMethod
                  (ct) => GoodbyeMethod("Anurag", ct)
                  );
            //Printing the result on the Console
            Console.WriteLine($"{content}");
        }
        static async Task<T> GenericOnlyOnePattern<T>(params Func<CancellationToken, Task<T>>[] functions)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var tasks = functions.Select(function => function(cancellationTokenSource.Token));
            var task = await Task.WhenAny(tasks);
            cancellationTokenSource.Cancel();
            return await task;
        }

        static async Task<string> HelloMethod(string name, CancellationToken token)
        {
            try
            {
                var WaitingTime = new Random().NextDouble() * 10 + 1;
                await Task.Delay(TimeSpan.FromSeconds(WaitingTime));
                token.ThrowIfCancellationRequested();
                string message = $"Hello {name}";
                return message;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{nameof(HelloMethod)} ({name}) has been canceled");
                throw;
            }
        }
        static async Task<string> GoodbyeMethod(string name, CancellationToken token)
        {
            try
            {
                var WaitingTime = new Random().NextDouble() * 10 + 1;
                await Task.Delay(TimeSpan.FromSeconds(WaitingTime));
                string message = $"Goodbye {name}";
                token.ThrowIfCancellationRequested();
                return message;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{nameof(GoodbyeMethod)} ({name}) has been canceled");
                throw;
            }
        }
    }
}
