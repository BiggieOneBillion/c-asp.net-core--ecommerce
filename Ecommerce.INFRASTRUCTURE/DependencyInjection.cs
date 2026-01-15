using System;
using System.Text;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Ecommerce.INFRASTRUCTURE.Repositories;
using Ecommerce.INFRASTRUCTURE.Services;
using Ecommerce.APPLICATION.Common.Interfaces;
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
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
        services.AddScoped<IProductPriceHistoryRepository, ProductPriceHistoryRepository>();
        services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();

        // Services
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IPermissionProvider, PermissionProvider>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHashers, BCryptPasswordHasher>();
        services.AddScoped<IEmailService, MockEmailService>();
        services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();

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

        // Authentication & Authorization
        var key = Encoding.ASCII.GetBytes(configureOptions["Jwt:Secret"] ?? "your-very-strong-secret-key-that-is-at-least-256-bits");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configureOptions["Jwt:Issuer"],
                ValidAudience = configureOptions["Jwt:Audience"],
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key)
            };
        });

        services.AddAuthorization();
        
        return services;
    }
}
