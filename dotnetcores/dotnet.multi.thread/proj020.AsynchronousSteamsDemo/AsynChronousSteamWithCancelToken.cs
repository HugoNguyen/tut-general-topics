using System.Runtime.CompilerServices;

namespace proj020
{
    internal class AsynChronousSteamWithCancelToken
    {
        public static async Task Run()
        {
            //Create an instance of CancellationTokenSource
            var CTS = new CancellationTokenSource();

            //Set the time when the token is going to cancel the stream
            CTS.CancelAfter(TimeSpan.FromSeconds(5));
            
            try
            {
                //Pass the Cancelllation Token to GenerateNames method
                await foreach (var name in GenerateNames(CTS.Token))
                {
                    Console.WriteLine(name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Dispose the CancellationTokenSource
                CTS.Dispose();
                CTS = null;
            }
            Console.ReadKey();
        }

        //This method accepts Cancellation Token as input parameter
        //Set its value to default
        private static async IAsyncEnumerable<string> GenerateNames(CancellationToken token = default)
        {
            //Check if request comes for Token Cancellation
            //if(token.IsCancellationRequested)
            //{
            //    token.ThrowIfCancellationRequested();
            //}
            //But here we just need to pass the token to Task.Delay method
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
