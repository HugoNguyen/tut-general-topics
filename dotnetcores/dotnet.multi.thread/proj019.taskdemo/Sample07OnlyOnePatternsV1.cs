namespace proj019
{
    internal class Sample07OnlyOnePatternsV1
    {
        public static void Run()
        {
            OnlyOnePattern();
            Console.ReadKey();
        }
        static async void OnlyOnePattern()
        {
            //Creating the Cancellation Token
            var CTS = new CancellationTokenSource();
            //Creating the list of names to process by the ProcessingName method
            List<string> names = new List<string>() { "Pranaya", "Anurag", "James", "Smith" };
            Console.WriteLine($"All Names");
            foreach (var item in names)
            {
                Console.Write($"{item} ");
            }
            //Creating the tasks by passing the name and cancellation token using Linq
            //It will invoke the ProcessingName method by passing name and cancellation token
            var tasks = names.Select(x => ProcessingName(x, CTS.Token));

            var task = await Task.WhenAny(tasks);
            //Fetch the first completed result
            var content = await task;
            //Cancel the token
            CTS.Cancel();
            //Print the content
            Console.WriteLine($"\n{content}");
        }
        static async Task<string> ProcessingName(string name, CancellationToken token)
        {
            try
            {
                //Creating Dynamic Waiting Time
                //The following statement will generate a number between 1 and 10 dynamically
                var WaitingTime = new Random().NextDouble() * 10 + 1;
                await Task.Delay(TimeSpan.FromSeconds(WaitingTime));
                string message = $"Hello {name}";
                token.ThrowIfCancellationRequested();
                return message;
            }
            catch(OperationCanceledException)
            {
                Console.WriteLine($"{nameof(ProcessingName)}({name}) has been canceled");
                throw;
            }
        }
    }
}
