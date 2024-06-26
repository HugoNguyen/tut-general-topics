using proj08_airconditioner.Products;

namespace proj08_airconditioner.Creators
{
    internal class WarmingFactory : AirConditionerFactory
    {
        public override IAirConditioner Create(double temperature) => new WarmingManager(temperature);
    }
}
}
