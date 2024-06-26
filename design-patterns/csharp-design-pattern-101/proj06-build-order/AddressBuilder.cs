namespace proj06_build_order
{
    internal class AddressBuilder
    {
        private string _street;
        private string _city;
        private string _zip;
        private string _state;
        private string _country;

        private AddressBuilder()
        {

        }

        public static AddressBuilder Empty() => new();

        public AddressBuilder Street(string street)
        {
            _street = street;
            return this;
        }

        public AddressBuilder City(string city)
        {
            _city = city;
            return this;
        }

        public AddressBuilder Zip(string zip)
        {
            _zip = zip;
            return this;
        }

        public AddressBuilder State(string state)
        {
            _state = state;
            return this;
        }

        public AddressBuilder Country(string country)
        {
            _country = country;
            return this;
        }

        public Address Build()
        {
            return new Address
            {
                Street = _street,
                City = _city,
                Zip = _zip,
                State = _state ?? "N/A",
                Country = _country
            };
        }
    }
}
