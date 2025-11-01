using Microsoft.Extensions.DependencyInjection;
using Cefalo.Csharp.Core.Services;
using Cefalo.Csharp.Core.Repositories;
using Cefalo.Csharp.Application.Services;
using Cefalo.Csharp.Infrastructure.Repositories;

namespace Cefalo.Csharp.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITicketService, TicketService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }
}