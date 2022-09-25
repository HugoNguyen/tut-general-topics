using bookstoreapp.blazor.webassembly.ui.Services.Base;

namespace bookstoreapp.blazor.webassembly.ui.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        Task Logout();
    }
}
