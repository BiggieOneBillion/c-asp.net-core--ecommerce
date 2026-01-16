using System;
using System.Reflection;
using Ecommerce.APPLICATION.Common.Behaviors;
using Ecommerce.APPLICATION.Services;
using FluentValidation;
using MediatR;
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
        
        // Register Pipeline Behaviors (order matters - they execute in registration order)
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        
        // Register Authentication Services
       
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<Ecommerce.APPLICATION.Common.Interfaces.IDiscountService, DiscountService>();
        
        return services;
    }
}

