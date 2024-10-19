using proj05_pizza_app.v1_Builder;
using proj05_pizza_app.v2_FluentBuilder;
using proj05_pizza_app.v3_ParentChildBuilder;
using proj05_pizza_app.v4_ProgessiveBuilder;

namespace proj05_pizza_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ExampleOfBuilder();
            //ExampleOfFluentBuilder();
            //ExampleOfParentChildBuilder();
            ExampleOfProgessiveBuilder();

            // Wait for user
            Console.ReadKey();
        }

        static void ExampleOfBuilder()
        {
            v1_Builder.PizzaBuilder builder;

            // Create a pizza assembly line
            AssemblyLine shop = new AssemblyLine();

            // Construct and display pizzas
            builder = new MeatFeastHot();
            shop.Assemble(builder);
            builder.Pizza.Display();

            builder = new HotNSpicyVeg();
            shop.Assemble(builder);
            builder.Pizza.Display();
        }

        static void ExampleOfFluentBuilder()
        {
            var pizzaBuilder = new FluentPizzaBuilder("Supreme");

            var pizzaSupreme = pizzaBuilder
                .WithDough("12-grain pizza dough")
                .WithSauce("Tomato base")
                .WithMeat("Pepperoni, Seasoned Minced Beef, Spicy Pork Sausage")
                .WithVeggie("Mushroom, Mixed Peppers, Red Onions")
                .WithCheese("Mozzarella")
                .WithExtra("Jalapenos")
                .Build();

            pizzaSupreme.Display();
        }

        static void ExampleOfParentChildBuilder()
        {
            var deal = new DealBuilder()
                .AddSide()
                    .WithItem("Spicy Loaded Pepperoni Wedges")
                    .WithDip("Mayo")
                    .WithSize("Large")
                    .BuildSide()
                .AddSide()
                    .WithItem("Spicy Cheesy Pepperoni Garlic Bread")
                    .WithDip("BBQ Sauce")
                    .WithSize("Large")
                    .BuildSide()
                .AddSalad()
                    .WithBase("Lettuce")
                    .WithVeggies("Cherry Tomatoes, Red Cabbage, Carrot")
                    .WithDressing("Garlic & Herbs")
                    .BuildSalad()
                .Build();
            deal.Display();
        }

        /// <summary>
        /// In the example below we will create a Pizza Deal, which will have some side dishes, a salad, a pizza, some drinks and a dessert.
        /// </summary>
        static void ExampleOfProgessiveBuilder()
        {
            var menuBuilder = new MenuDealBuilder();
            var menu = menuBuilder
                .AddSideDish()
                    .WithItem("Breaded All-White Chicken Breast, Baked in Stone Oven")
                    .WithDip("Frank's Spicy Buffalo")
                    .WithSize("Large")
                .AddSideDish()
                    .WithItem("Gluten Free Corn Tortilla Chips")
                    .WithDip("Guacamole")
                    .WithSize("Large")
                .AddSaladDish()
                    .WithBase("Fresh Mixed Lettuce")
                    .WithVeggies("Chopped Onions, Green Bell Peppers, Black Olives, Mushrooms")
                    .WithMeats("Fajita Chicken, Bacon Bits")
                    .WithCheeses("Mozzarella")
                    .WithDressing("Zesty Italian")
                .AddPizzaDish()
                    .WithDough("Sourdough with Cream Cheese Crust")
                    .WithSauce("Tomato Sauce")
                    .WithMeats("Beef, Sausage, Pepperoni")
                    .WithVeggies("Mushrooms, Green Bell Peppers, Onions")
                    .WithCheeses("Mozzarella")
                .AddDrink()
                    .WithItem("Soda")
                    .WithSize("Large")
                .AddDesert()
                    .WithItem("Cookie Dough Ice Cream")
                    .WithSize("Large")
                .Build();

            menu.Display();
        }
    }
}
