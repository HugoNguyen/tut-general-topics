namespace bookify.domain.Apartments;

/// <summary>
/// Tips: Use record to define a ValueObject
/// </summary>
public record Address(
    string Country,
    string State,
    string ZipCode,
    string City,
    string Street);
