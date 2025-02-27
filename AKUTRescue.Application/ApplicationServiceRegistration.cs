using System.Reflection;
using FluentValidation;
using AKUTRescue.Application.Features.Members.Rules;
using AKUTRescue.Application.Features.Teams.Rules;
using AKUTRescue.Application.Features.Warehouses.Rules;
using AKUTRescue.Application.Features.Profiles.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace AKUTRescue.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Business Rules registrations
        services.AddScoped<MemberBusinessRules>();
        services.AddScoped<TeamBusinessRules>();
        services.AddScoped<WarehouseBusinessRules>();
        services.AddScoped<ProfileBusinessRules>();

        return services;
    }
} 