using bookify.infrastructure;
using Microsoft.EntityFrameworkCore;

namespace bookify.api.Extensions;

public static class ApplicationBuilderExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}