using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SourceSafe.Application.Common.Behaviours;
using System.Reflection;

namespace SourceSafe.Application;

public static class DenpendecyInjectcion
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
