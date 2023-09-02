using bookify.application.Abstractions.Behaviors;
using bookify.domain.Bookings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace bookify.application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

        });

        // Register Validators
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient<PricingService>();

        return services;
    }
}
