using Microsoft.Extensions.Options;

namespace github.api.DelegatingHandlers;

public sealed class GitHubAuthenticationHandler : DelegatingHandler
{
    private readonly GitHubSettings _gitHubSettings;

    public GitHubAuthenticationHandler(IOptions<GitHubSettings> options)
    {
        _gitHubSettings = options.Value;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Add("Authorization", _gitHubSettings.AccessToken);
        request.Headers.Add("User-Agent", _gitHubSettings.UserAgent);
        request.Headers.Add("X-GitHub-Api-Version", _gitHubSettings.ApiVersion);

        return base.SendAsync(request, cancellationToken);
    }
}
