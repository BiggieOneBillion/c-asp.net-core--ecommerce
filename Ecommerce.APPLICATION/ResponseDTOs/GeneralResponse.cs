namespace Ecommerce.APPLICATION.ResponseDTOs;

public class GeneralResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }

    public static GeneralResponse<T> CreateSuccess(T? data, string message = "Success", int statusCode = 200)
    {
        return new GeneralResponse<T>
        {
            Data = data,
            Success = true,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static GeneralResponse<T> CreateFailure(string message, int statusCode = 400)
    {
        return new GeneralResponse<T>
        {
            Data = default,
            Success = false,
            Message = message,
            StatusCode = statusCode
        };
    }
}