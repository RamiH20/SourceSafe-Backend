using AutoMapper;

namespace SourceSafe.API.Common.Mapping;
public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddScoped<IMapper, Mapper>();
        return services;
    }
}
