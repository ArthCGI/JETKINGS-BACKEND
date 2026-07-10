using FluentValidation;
using JetKings.Application.Interfaces.IServices;
using JetKings.Application.Mappings;
using JetKings.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JetKings.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(UserMappingProfile)));
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceExtensions).Assembly);
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
