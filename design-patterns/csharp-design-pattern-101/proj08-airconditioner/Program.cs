using proj08_airconditioner.Creators;

namespace proj08_airconditioner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirConditioner
                .InitializeFactories()
                .ExecuteCreation(Actions.Cooling, 22.5)
                .Operate();
        }
    }
}
