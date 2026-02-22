using Ecommerce.APPLICATION;
using Ecommerce.INFRASTRUCTURE;
using Ecommerce.INFRASTRUCTURE.BackgroundJobs;
using Ecommerce.INFRASTRUCTURE.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// APPLICATION Layer - Registers:
// - MediatR (CQRS Commands & Queries)
// - FluentValidation (Input validation)
// - AutoMapper (Entity-to-DTO mapping)
// - Authentication Services (JWT, Password)
builder.Services.AddApplication();

// INFRASTRUCTURE Layer - Registers:
// - DbContext (Entity Framework Core)
// - Repositories (Data access)
// - Database connection
builder.Services.AddInfrastructure(builder.Configuration);

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React/Vite frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Allow cookies if needed
    });
});


// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Ecommerce API",
        Version = "v1",
        Description = "RESTful API for Modern Ecommerce Platform with CQRS architecture",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Ecommerce Support",
            Email = "support@ecommerce.com"
        }
    });

    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
    });

    // Add the custom filter to document permissions
    options.OperationFilter<Ecommerce.API.Filters.SwaggerAuthorizeCheckOperationFilter>();

    // Use XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Management API v1");
        options.RoutePrefix = string.Empty; // Serve Swagger UI at root (http://localhost:5000/)
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<Ecommerce.API.Middleware.SecurityHeadersMiddleware>();
app.UseMiddleware<Ecommerce.API.Middleware.RateLimitingMiddleware>();

app.MapControllers();

// Hangfire Dashboard
// app.UseHangfireDashboard();

// // Schedule Recurring Job
// using (var scope = app.Services.CreateScope())
// {
//     var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
//     recurringJobManager.AddOrUpdate<ProcessOutboxMessagesJob>(
//         "outbox-processor",
//         job => job.Execute(),
//         Cron.Minutely);
// }

app.MapGet("/", () => "Ecommerce Backend is running. Visit /swagger for documentation.");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // This will apply any pending migrations on startup
    context.Database.Migrate();
}
  
// if (app.Environment.IsDevelopment())
// {
//     using var scope = app.Services.CreateScope();
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     try
//     {
//         await dbContext.Database.MigrateAsync();
        
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine( "Database migration failed - ensure SQL Server is running");
//     }
// }

app.Logger.LogInformation("Starting Ecommerce API...");
app.Logger.LogInformation("CQRS Architecture: MediatR + FluentValidation + AutoMapper");
app.Logger.LogInformation("Swagger UI available at: http://localhost:5000/");
app.Logger.LogInformation("API Base URL: http://localhost:5000/api/v1");

app.Run();


