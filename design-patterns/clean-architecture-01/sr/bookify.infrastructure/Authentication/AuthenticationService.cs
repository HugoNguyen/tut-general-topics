using bookify.application.Abstractions.Authentication;
using bookify.domain.Users;
using bookify.infrastructure.Authentication.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace bookify.infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    private const string PasswordCredentialType = "password";

    /// <summary>
    /// Use to access KEYCLOAK Restful API
    /// </summary>
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Send user model to Keycloak server to create user
    ///     {host}/admin/realms/user
    ///     Intercept httpclient, add admin token
    ///         via AdminAuthorizationDelegatingHandler
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);

        userRepresentationModel.Credentials = new CredentialRepresentationModel[]
        {
            new()
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);

        return ExtractIdentityIdFromLocationHeader(response);
    }

    private string ExtractIdentityIdFromLocationHeader(HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header can't be null");
        }

        var userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName, 
            StringComparison.InvariantCultureIgnoreCase);

        var userIdentityId = locationHeader.Substring(
            userSegmentValueIndex + usersSegmentName.Length);

        return userIdentityId;
    }
}
