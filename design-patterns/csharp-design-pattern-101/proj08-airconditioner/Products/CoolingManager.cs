namespace proj08_airconditioner.Products
{
    internal class CoolingManager : IAirConditioner
    {
        private readonly double _temperature;
        public CoolingManager(double temperature)
        {
            _temperature = temperature;
        }
        public void Operate()
        {
            Console.WriteLine($"Cooling the room to the required temperature of {_temperature} degrees");
        }
    }
}
