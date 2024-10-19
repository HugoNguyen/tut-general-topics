namespace proj06_build_order
{
    internal class Address
    {
        public string Street {  get; init; }
        public string City { get; init; }
        public string Zip {  get; init; }
        public string State {  get; init; }
        public string Country { get; init; }

        public override string ToString()
        {
            return $"[{Street}, {City}, {Zip}, {State}, {Country}]";
        }
    }
}
