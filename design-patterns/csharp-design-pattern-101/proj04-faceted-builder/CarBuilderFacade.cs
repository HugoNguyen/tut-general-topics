namespace proj04_faceted_builder
{
    /// <summary>
    /// We instantiate the Car object, which we want to build and expose it through the Build method.
    /// </summary>
    public class CarBuilderFacade
    {
        protected Car Car { get; set; }

        public CarBuilderFacade()
        {
            Car = new Car();
        }

        public Car Build() => Car;

        public CarInfoBuilder Info => new CarInfoBuilder(Car);
        public CarAddressBuilder Address => new CarAddressBuilder(Car);
    }
}
