namespace proj021.paralleldemos
{
    internal class Sample13StandardLinqVsParallelLinq
    {
        public static void Run()
        {
            StandardLinq();
            Console.WriteLine();
            ParallelLinq();
            Console.ReadLine();
        }

        static void StandardLinq()
        {
            Console.WriteLine($"{nameof(StandardLinq)}");
            //Creating a Collection of integer numbers
            var numbers = Enumerable.Range(1, 40);
            //Fetching the List of Even Numbers using LINQ
            var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();
            Console.WriteLine("Even Numbers Between 1 and 40");
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }
        }

        static void ParallelLinq()
        {
            Console.WriteLine($"{nameof(ParallelLinq)}");
            //Creating a Collection of integer numbers
            var numbers = Enumerable.Range(1, 40);
            //Fetching the List of Even Numbers using PLINQ
            //PLINQ means we need to use AsParallel()
            var evenNumbers = numbers
                .AsParallel()
                .AsOrdered() //Original Order of the numbers vs OrderBy
                .Where(x => x % 2 == 0).ToList();
            Console.WriteLine("Even Numbers Between 1 and 40");
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
