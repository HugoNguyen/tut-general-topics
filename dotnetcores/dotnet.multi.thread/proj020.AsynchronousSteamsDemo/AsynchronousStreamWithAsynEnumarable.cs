namespace proj020
{
    internal class AsynchronousStreamWithAsynEnumarable
    {
        public static async Task Run()
        {
            await foreach (var name in GenerateNames())
            {
                Console.WriteLine(name);
            }

            Console.ReadKey();
        }

        private static async IAsyncEnumerable<string> GenerateNames()
        {
            Console.WriteLine($"{nameof(GenerateNames)} begin");
            await Task.Delay(TimeSpan.FromSeconds(1));
            yield return "Anurag";
            await Task.Delay(TimeSpan.FromSeconds(2));
            yield return "Pranaya";
            await Task.Delay(TimeSpan.FromSeconds(3));
            yield return "Sambit";
            Console.WriteLine($"{nameof(GenerateNames)} end");
        }
    }
}
