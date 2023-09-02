using bookify.application.Abstractions.Email;

namespace bookify.infrastructure.Email;
internal sealed class EmailService : IEmailService
{
    public Task SendAsync(domain.Users.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
