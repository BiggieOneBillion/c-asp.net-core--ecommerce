using System;
using System.Reflection;
using Ecommerce.APPLICATION.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.APPLICATION;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        // Register AutoMapper
        services.AddAutoMapper(assembly);
        
        // MediatR - Register all handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        
        // Register FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Register Authentication Services
       
        services.AddScoped<IPasswordService, PasswordService>();
        
        return services;
    }
}

