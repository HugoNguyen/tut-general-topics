﻿namespace proj05_pizza_app.v4_ProgessiveBuilder
{
    public class Dessert
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
            Console.WriteLine("\n--------- Dessert Dish ---------");
            Console.WriteLine($"Item: {Item}");
            Console.WriteLine($"Size: {Size}");
        }
    }
}
