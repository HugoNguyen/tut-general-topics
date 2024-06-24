using System.Diagnostics;

namespace proj019
{
    /// <summary>
    /// Process multiple credit cards
    /// First, Generate 10 credit card
    /// Second, Process these card at same time
    /// Wait all task complete
    /// Output: Processing of 10 creditcard Done in 10.16s
    /// </summary>
    public class Sample02ExecuteMultipleTasksV2
    {
        public static void Run()
        {
            Console.WriteLine($"Main Thread Started");
            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(10);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");

            ProcessCreditCards(creditCards);

            Console.WriteLine($"Main Thread Completed");
            Console.ReadKey();
        }

        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var creditCard in creditCards)
            {
                var response = await ProcessCard(creditCard);
            }
            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
        }

        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            //Here we can do any API Call to Process the Credit Card
            //But for simplicity we are just delaying the execution for 1 second
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            Console.WriteLine($"Credit Card Number: {creditCard.CardNumber} Processed");
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
