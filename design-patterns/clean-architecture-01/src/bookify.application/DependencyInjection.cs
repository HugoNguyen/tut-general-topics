using bookify.domain.Bookings;
using Microsoft.Extensions.DependencyInjection;

namespace bookify.application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<PricingService>();

        return services;
    }
}
