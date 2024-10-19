namespace proj05_pizza_app.v4_ProgessiveBuilder
{
    public class Side
    {
        public string Item
        {
            get;
            set;
        }
        public string Dip
        {
            get;
            set;
        }
        public string Size
        {
            get;
            set;
        }

        public void Display()
        {
            Console.WriteLine("\n--------- Side Dish ---------");
            Console.WriteLine($"Item: {Item}");
            Console.WriteLine($"Dip: {Dip}");
            Console.WriteLine($"Size: {Size}");
        }
    }
}
