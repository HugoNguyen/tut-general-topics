using bookify.domain.Abstractions;

namespace bookify.domain.Apartments;
public static class ApartmentErrors
{
    public static Error NotFound = new(
        "Property.NotFound",
        "The property with the specified identifier was not found");
}
