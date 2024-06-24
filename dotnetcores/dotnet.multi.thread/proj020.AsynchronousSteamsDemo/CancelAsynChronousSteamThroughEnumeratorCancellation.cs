using System.Runtime.CompilerServices;

namespace proj020
{
    internal class CancelAsynChronousSteamThroughEnumeratorCancellation
    {
        public static async Task Run()
        {
            //Here we are receiving an IAsyncEnumerable.
            //This is just a represenatation of the stream,
            //But we are not running the stream here
            var namesEnumerable = GenerateNames();
            await ProcessNames(namesEnumerable);
            Console.ReadKey();
        }

        private static async Task ProcessNames(IAsyncEnumerable<string> namesEnumerable)
        {
            //Creating the CancellationTokenSource instance
            var CTS = new CancellationTokenSource();
            //Setting the time interval when the token is going to be cancelled
            CTS.CancelAfter(TimeSpan.FromSeconds(5));
            //Iterating the IAsyncEnumerable 
            //Passing the Cancellation Token using WithCancellation method
            try
            {
                await foreach (var name in namesEnumerable.WithCancellation(CTS.Token))
                {
                    Console.WriteLine($"{name} - Processed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CTS.Dispose();
                CTS = null;
            }
        }

        private static async IAsyncEnumerable<string> GenerateNames([EnumeratorCancellation] CancellationToken token = default)
        {
            yield return "Anurag";
            await Task.Delay(TimeSpan.FromSeconds(3), token);
            yield return "Pranaya";
            await Task.Delay(TimeSpan.FromSeconds(3), token);
            yield return "Sambit";
            await Task.Delay(TimeSpan.FromSeconds(3), token);
            yield return "Rakesh";
        }
    }
}
