using bookify.domain.Abstractions;

namespace bookify.domain.Reviews;
public sealed class Rating
{
    public static readonly Error Invalid = new("Rating.Invalid", "The rating is invalid");

    private Rating(int value) => Value = value;

    public int Value { get; init; }

    public static Result<Rating> Create(int value)
    {
        if(value < 1 ||  value > 5)
        {
            return Result.Failure<Rating>(Invalid);
        }

        // Implicit convert to Result, Result.cs line 58
        return new Rating(value);
    }
}
