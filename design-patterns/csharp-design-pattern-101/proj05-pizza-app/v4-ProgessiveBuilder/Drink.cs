﻿namespace proj05_pizza_app.v4_ProgessiveBuilder
{
    public class Drink
    {
        public string Item
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
            Console.WriteLine("\n--------- Drink Dish ---------");
            Console.WriteLine($"Item: {Item}");
            Console.WriteLine($"Size: {Size}");
        }
    }
}
