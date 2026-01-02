using Ecommerce.APPLICATION;
using Ecommerce.INFRASTRUCTURE;
using Ecommerce.INFRASTRUCTURE.BackgroundJobs;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

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
        Title = "Project Management API",
        Version = "v1",
        Description = "RESTful API for Project Management Platform with CQRS architecture",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Project Management Team"
        }
    });
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

app.MapControllers();

// Hangfire Dashboard
app.UseHangfireDashboard();

// Schedule Recurring Job
using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<ProcessOutboxMessagesJob>(
        "outbox-processor",
        job => job.Execute(),
        Cron.Minutely);
}

app.Logger.LogInformation("Starting Ecommerce API...");
app.Logger.LogInformation("CQRS Architecture: MediatR + FluentValidation + AutoMapper");
app.Logger.LogInformation("Swagger UI available at: http://localhost:5000/");
app.Logger.LogInformation("API Base URL: http://localhost:5000/api/v1");

app.Run();


