namespace proj06_build_order
{
    internal class Order
    {
        public int Number { get; init; }
        public DateTime CreateOn { get; init; }
        public Address ShippingAddress { get; init; }

        public override string ToString()
        {
            return $"{Number} created {CreateOn} shipped to {ShippingAddress}";
        }
    }
}
