namespace proj020
{
    internal class CancelAsynchronousStreamWithBreak
    {
        public static async Task Run()
        {
            await foreach (var name in GenerateNames())
            {
                Console.WriteLine(name);
                //Some condition to break the asynchronous stream
                if (name == "Pranaya")
                {
                    Console.WriteLine("Cancel this stream");
                    break;
                }
            }
            Console.ReadKey();
        }
        private static async IAsyncEnumerable<string> GenerateNames()
        {
            yield return "Anurag";
            await Task.Delay(TimeSpan.FromSeconds(3));
            yield return "Pranaya";
            await Task.Delay(TimeSpan.FromSeconds(3));
            yield return "Sambit";
            await Task.Delay(TimeSpan.FromSeconds(3));
            yield return "Rakesh";
        }
    }
}
