namespace bookify.infrastructure.Authentication.Models;

public sealed class CredentialRepresentationModel
{
    public string Value { get; set; } = string.Empty;
    public bool Temporary { get; set; } = false;
    public string Type { get; set; } = string.Empty;
}