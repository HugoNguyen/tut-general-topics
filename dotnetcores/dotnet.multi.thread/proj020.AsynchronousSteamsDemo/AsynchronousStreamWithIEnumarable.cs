namespace proj020
{
    internal class AsynchronousStreamWithIEnumarable
    {
        public static void Run()
        {
            foreach (var name in GenerateNames())
            {
                //You can do anything with the name
                //for example printing the name on the console
                Console.WriteLine(name);
            }
            Console.ReadKey();
        }

        //This method is going to generate names over a period of time
        private static IEnumerable<string> GenerateNames()
        {
            Console.WriteLine($"{nameof(GenerateNames)} begin");
            Thread.Sleep(1000);
            yield return "Anurag";
            Thread.Sleep(2000);
            yield return "Pranaya";
            Thread.Sleep(3000);
            yield return "Sambit";
            Console.WriteLine($"{nameof(GenerateNames)} end");
        }
    }
}
