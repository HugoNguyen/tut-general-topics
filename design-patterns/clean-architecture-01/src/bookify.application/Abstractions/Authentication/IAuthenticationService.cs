using bookify.domain.Users;

namespace bookify.application.Abstractions.Authentication;
public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
