using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Ecommerce.INFRASTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Hangfire;
using Hangfire.PostgreSql;
using Ecommerce.INFRASTRUCTURE.BackgroundJobs;

namespace Ecommerce.INFRASTRUCTURE;

public static  class DependencyInjection
{
     public static IServiceCollection AddInfrastructure(this IServiceCollection services,  IConfiguration configureOptions)
    {
        // configure Nosql to handle datetime correctly
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // DATABASE
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(
                configureOptions.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

         // Repositories
       
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
        services.AddScoped<IProductPriceHistoryRepository, ProductPriceHistoryRepository>();
        services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Background Jobs
        services.AddScoped<ProcessOutboxMessagesJob>();

        // Hangfire
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(configureOptions.GetConnectionString("DefaultConnection")));

        services.AddHangfireServer();
        
        return services;
    }
}
