using proj08_airconditioner.Products;

namespace proj08_airconditioner.Creators
{
    internal class CoolingFactory : AirConditionerFactory
    {
        public override IAirConditioner Create(double temperature) => new CoolingManager(temperature);
    }
}
}
