using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Persistence.Contexts;
using AKUTRescue.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AKUTRescue.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<AKUTRescueDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IMemberDetailRepository, MemberDetailRepository>();

        return services;
    }
} 