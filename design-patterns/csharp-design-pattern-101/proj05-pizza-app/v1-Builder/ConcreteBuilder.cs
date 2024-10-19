namespace proj05_pizza_app.v1_Builder
{
    /// 
    /// A ConcreteBuilder class
    /// 
    class MeatFeastHot : PizzaBuilder
    {
        public MeatFeastHot()
        {
            pizza = new Pizza("Meat Feast Hot");
        }

        public override void AddDough()
        {
            pizza["dough"] = "Wheat pizza dough";
        }

        public override void AddSauce()
        {
            pizza["sauce"] = "Tomato base";
        }

        public override void AddMeats()
        {
            pizza["meats"] = "Pepperoni, Ham, Beef, Chicken";
        }

        public override void AddCheeses()
        {
            pizza["cheeses"] = "Signature triple cheese blend, mozzarella";
        }

        public override void AddVeggies()
        {
            pizza["veggies"] = "";
        }

        public override void AddExtras()
        {
            pizza["extras"] = "jalapenos";
        }
    }

    /// 
    /// A ConcreteBuilder class
    /// 
    class HotNSpicyVeg : PizzaBuilder
    {
        public HotNSpicyVeg()
        {
            pizza = new Pizza("Hot 'N' Spicy Veg");
        }

        public override void AddDough()
        {
            pizza["dough"] = "12-grain pizza dough";
        }

        public override void AddSauce()
        {
            pizza["sauce"] = "Tomato base";
        }

        public override void AddMeats()
        {
            pizza["meats"] = "";
        }

        public override void AddCheeses()
        {
            pizza["cheeses"] = "Signature triple cheese blend, mozzarella";
        }

        public override void AddVeggies()
        {
            pizza["veggies"] = "Mushrooms, Peppers, Red Onions";
        }

        public override void AddExtras()
        {
            pizza["extras"] = "Jalapenos";
        }
    }
}
