namespace proj06_build_order
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var order = OrderBuilder.Empty()
                .WithNumber(10)
                .CreateOn(DateTime.UtcNow)
                .ShippedTo(b => b
                    .Street("street")
                    .City("city")
                    .Zip("zip")
                    .Country("country"))
                .Buid();

            Console.WriteLine(order.ToString());

            List<Order[]> orders = Enumerable
                .Range(0, 10)
                .Select(number => 
                    OrderBuilder.Empty()
                        .WithNumber(number)
                        .CreateOn(DateTime.UtcNow)
                        .ShippedTo(b => b
                            .Street("street")
                            .City("city")
                            .Zip("zip")
                            .Country("country"))
                        .Buid())
                .Chunk(2)
                .ToList();

            orders.ForEach(arr =>
            {
                foreach (var o in arr)
                {
                    Console.WriteLine(o.ToString());
                }
            });
        }
    }
}
