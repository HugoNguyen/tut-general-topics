namespace bookify.application.Abstractions.Clock;

/// <summary>
/// The benefic of this approach is that this is completely testable
///     Solve risky when do with datetime object
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
