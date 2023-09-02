using bookify.domain.Abstractions;
using bookify.domain.Shared;

namespace bookify.domain.Apartments;

/// <summary>
/// Tips: seal all class if at that time you don't intend to inherit them.
/// </summary>
public sealed class Apartment : Entity<ApartmentId>
{
    private Apartment() { }

    public Apartment(
        ApartmentId id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleanFee,
        List<Amenity> amenities)
        : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleanFee;
        Amenities = amenities;
    }

    /// <summary>
    /// Tip: Convert string to ValueObject
    /// </summary>
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Address Address { get; private set; }
    public Money Price { get; private set; }
    public Money CleaningFee { get; private set; }
    public DateTime? LastBookedOnUtc { get; internal set; }
    public List<Amenity> Amenities { get; private set; } = new();
}
