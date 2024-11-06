using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SourceSafe.Application;

public static class DenpendecyInjectcion
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
