namespace bookify.domain.Reviews;
public record ReviewId(Guid Value)
{
    public static ReviewId New() => new(Guid.NewGuid());
}
