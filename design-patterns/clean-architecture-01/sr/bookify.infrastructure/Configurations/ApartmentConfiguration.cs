using bookify.domain.Apartments;
using bookify.domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookify.infrastructure.Configurations;

/// <summary>
/// Using Fluent configuration approach
/// </summary>
internal sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        // 1. Map to table name in database
        builder.ToTable("apartments");

        // 2. Define primary key
        builder.HasKey(apartment => apartment.Id);

        builder.Property(apartment => apartment.Id)
            .HasConversion(apartmentId => apartmentId.Value, value => new ApartmentId(value));

        // 3. Mapping a complex object (has many property).
        //// properties of complex object will be map to a set of columns in the table owning enitity, is apartments
        //// If it is a collection, will use OwnsCollection. In that case, it will be map to a separate table
        builder.OwnsOne(apartment => apartment.Address);

        builder.Property(apartment => apartment.Name)
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(apartment => apartment.Description)
            .HasMaxLength(2000)
            .HasConversion(description => description.Value, value => new Description(value));

        builder.OwnsOne(apartment => apartment.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(apartment => apartment.CleaningFee, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.Property<uint>("Version").IsRowVersion();
    }
}
