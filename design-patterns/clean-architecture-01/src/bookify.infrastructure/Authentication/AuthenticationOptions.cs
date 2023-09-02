namespace bookify.infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Audience { get; init; } = string.Empty;

    public string MetadataUrl { get; init; } = string.Empty;

    public bool RequireHttpsMetadata { get; init; }

    public string ValidIssuer { get; set; } = string.Empty;
}