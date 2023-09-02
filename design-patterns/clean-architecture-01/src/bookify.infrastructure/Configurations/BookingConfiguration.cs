using bookify.domain.Apartments;
using bookify.domain.Bookings;
using bookify.domain.Shared;
using bookify.domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookify.infrastructure.Configurations;
internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(booking => booking.Id);

        builder.Property(booking => booking.Id)
            .HasConversion(bookingId => bookingId.Value, value => new BookingId(value));

        builder.OwnsOne(booking => booking.PriceForPeriod, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.CleaningFee, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.AmenitiesUpCharge, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.TotalPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.Duration);

        //Don't need to add a conversion here. TESTED
        //builder.Property(booking => booking.ApartmentId)
        //    .HasConversion(apartmentId => apartmentId.Value, value => new ApartmentId(value));

        builder.HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(booking => booking.ApartmentId);

        //Don't need to add a conversion here. TESTED
        //builder.Property(booking => booking.UserId)
        //    .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(booking => booking.UserId);
    }
}
