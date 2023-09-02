using bookify.application.Abstractions.Messaging;

namespace bookify.application.Users.RegisterUser;
public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password) : ICommand<Guid>;
