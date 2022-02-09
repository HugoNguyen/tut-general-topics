using bookstoreapp.blazor.server.ui.Services.Base;

namespace bookstoreapp.blazor.server.ui.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        Task Logout();
    }
}
