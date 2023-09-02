using System.Text.Json.Serialization;

namespace github.api;
public class GitHubUser
{
    public string? Login { get;set; }
    public long? Id { get;set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get;set; }
    [JsonPropertyName("avartar_url")]
    public string? AvatartUrl { get;set; }
    public string? Url { get; set; }
    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }
    [JsonPropertyName("followers_url")]
    public string? FollowersUrl { get; set; }
    [JsonPropertyName("following_url")]
    public string? FollowingUrl { get; set; }
}