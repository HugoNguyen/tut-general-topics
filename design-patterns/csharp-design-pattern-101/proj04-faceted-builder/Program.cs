namespace proj04_faceted_builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                  .WithType("BMW")
                  .WithColor("Black")
                  .WithNumberOfDoors(5)
                .Address
                  .InCity("Leipzig ")
                  .AtAddress("Some address 254")
                .Build();
            Console.WriteLine(car);
        }
    }
}
