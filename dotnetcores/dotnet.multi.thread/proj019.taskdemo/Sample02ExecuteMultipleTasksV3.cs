using System.Diagnostics;

namespace proj019
{
    /// <summary>
    /// Offloading the Current Thread
    /// First Generate 100,000
    /// Before using offloading Output:
    ///     Main Thread execution time 8s
    ///     Processing of 100,000 Creditcard Done in 9s
    /// After using offloading Output:
    ///     Main Thread execution time 0.102s
    ///     Processing of 100,000 Creditcard Done in 9s
    /// </summary>
    public class Sample02ExecuteMultipleTasksV3
    {
        public static void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"Main Thread Started");
            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(100000);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");

            ProcessCreditCards(creditCards);

            Console.WriteLine($"Main Thread Completed");
            stopwatch.Stop();
            Console.WriteLine($"Main Thread Execution Time {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
            Console.ReadKey();
        }
        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = new List<Task<string>>();

            // Offloading
            await Task.Run(() =>
            {
                foreach (var creditCard in creditCards)
                {
                    var response = ProcessCard(creditCard);
                    tasks.Add(response);
                }
            });

            //It will execute all the tasks concurrently
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
        }

        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            return message;
        }

        public class CreditCard
        {
            public string CardNumber { get; set; }
            public string Name { get; set; }
            public static List<CreditCard> GenerateCreditCards(int number)
            {
                List<CreditCard> creditCards = new List<CreditCard>();
                for (int i = 0; i < number; i++)
                {
                    CreditCard card = new CreditCard()
                    {
                        CardNumber = "10000000" + i,
                        Name = "CreditCard-" + i
                    };
                    creditCards.Add(card);
                }
                return creditCards;
            }
        }
    }
}
