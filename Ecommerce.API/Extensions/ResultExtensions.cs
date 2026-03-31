using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ProcessResult<T>(this Result<GeneralResponse<T>> result, ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            return result.Value.StatusCode switch
            {
                201 => controller.StatusCode(201, result.Value),
                204 => controller.NoContent(),
                _ => controller.Ok(result.Value)
            };
        }

        var statusCode = result.Error.Code switch
        {
            "Validation.Failed" => 400,
            "Error.NotFound" => 404,
            "Error.Unauthorized" => 401,
            "Error.Conflict" => 409,
            _ => 400
        };

        // We use object here to avoid type mismatch if the handler returns GeneralResponse<SpecificType>
        var response = GeneralResponse<object>.CreateFailure(
            result.Error.Message,
            statusCode);

        return controller.StatusCode(statusCode, response);
    }
}
