using bookify.application.Abstractions.Messaging;

namespace bookify.application.Users.LogInUser;
public sealed record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
