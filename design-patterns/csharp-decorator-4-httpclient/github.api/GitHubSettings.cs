public sealed class GitHubSettings
{
    public static string ConfigurationSection = "GitHub";
    public string AccessToken { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string ApiVersion { get;set; } = string.Empty;
}