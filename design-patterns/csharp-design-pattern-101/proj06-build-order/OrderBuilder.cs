namespace proj06_build_order
{
    internal class OrderBuilder
    {
        private int _number;
        private DateTime _createOn;
        private readonly AddressBuilder _addressBuilder = AddressBuilder.Empty();

        private OrderBuilder()
        {

        }

        public static OrderBuilder Empty() => new();

        public OrderBuilder WithNumber(int number)
        {
            _number = number;
            return this;
        }

        public OrderBuilder CreateOn(DateTime createOn)
        {
            _createOn = createOn;
            return this;
        }

        public OrderBuilder ShippedTo(Action<AddressBuilder> action)
        {
            action(_addressBuilder);
            return this;
        }

        public Order Buid()
        {
            return new Order
            {
                Number = _number,
                CreateOn = _createOn,
                ShippingAddress = _addressBuilder.Build()
            };
        }
    }
}
