namespace proj05_pizza_app.v1_Builder
{
    /// 
    /// The Director class
    /// 
    public class AssemblyLine
    {
        // Builder uses a complex series of steps
        public void Assemble(PizzaBuilder pizzaBuilder)
        {
            pizzaBuilder.AddDough();
            pizzaBuilder.AddSauce();
            pizzaBuilder.AddCheeses();
            pizzaBuilder.AddMeats();
            pizzaBuilder.AddVeggies();
            pizzaBuilder.AddExtras();
        }
    }
}
