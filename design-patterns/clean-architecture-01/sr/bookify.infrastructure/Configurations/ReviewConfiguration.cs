using bookify.domain.Apartments;
using bookify.domain.Bookings;
using bookify.domain.Reviews;
using bookify.domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookify.infrastructure.Configurations;
internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(review => review.Id);

        builder.Property(review => review.Id)
            .HasConversion(reviewId => reviewId.Value, value => new ReviewId(value));

        builder.Property(review => review.Rating)
            .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

        builder.Property(review => review.Comment)
            .HasMaxLength(200)
            .HasConversion(comment => comment.Value, value => new Comment(value));

        //Don't need to add a conversion here. TESTED
        builder.HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(review => review.ApartmentId);

        //Don't need to add a conversion here. TESTED
        builder.HasOne<Booking>()
            .WithMany()
            .HasForeignKey(review => review.BookingId);

        //Don't need to add a conversion here. TESTED
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);
    }
}