using System.Reflection;
using Ecommerce.APPLICATION.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ecommerce.API.Filters;

public class SwaggerAuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.DeclaringType == null) return;

        // Check if the endpoint itself has Authorize or HasPermission on the controller/action
        var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                           context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        // But more importantly, check the MediatR request parameter
        var requestParameters = context.MethodInfo.GetParameters();
        var mediatrRequest = requestParameters
            .FirstOrDefault(p => p.ParameterType.GetInterfaces()
                .Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(MediatR.IRequest<>) || i.GetGenericTypeDefinition() == typeof(MediatR.IRequest))));

        if (mediatrRequest != null)
        {
            var permissionAttributes = mediatrRequest.ParameterType.GetCustomAttributes<HasPermissionAttribute>(true);
            if (permissionAttributes.Any())
            {
                var permissions = string.Join(", ", permissionAttributes.Select(a => a.Permission));
                operation.Description = $"**Required Permissions:** {permissions}<br/>" + (operation.Description ?? "");
                hasAuthorize = true;
            }
        }

        if (hasAuthorize)
        {
            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
        }
    }
}
