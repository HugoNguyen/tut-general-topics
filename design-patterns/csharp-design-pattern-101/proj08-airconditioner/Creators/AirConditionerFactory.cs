using proj08_airconditioner.Products;

namespace proj08_airconditioner.Creators
{
    internal abstract class AirConditionerFactory
    {
        public abstract IAirConditioner Create(double temperature);
    }
}
