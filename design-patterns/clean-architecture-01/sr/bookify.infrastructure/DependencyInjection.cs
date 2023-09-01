using bookify.application.Abstractions.Clock;
using bookify.application.Abstractions.Data;
using bookify.application.Abstractions.Email;
using bookify.domain.Abstractions;
using bookify.domain.Apartments;
using bookify.domain.Bookings;
using bookify.domain.Users;
using bookify.infrastructure.Authentication;
using bookify.infrastructure.Clock;
using bookify.infrastructure.Data;
using bookify.infrastructure.Email;
using bookify.infrastructure.Repositories;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bookify.infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<IEmailService, EmailService>();
        
        AddPersistence(services, configuration);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
                    configuration.GetConnectionString("Database") ??
                    throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IApartmentRepository, ApartmentRepository>();

        services.AddScoped<IBookingRepository, BookingRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }
}
