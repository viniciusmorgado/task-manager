using System.Net;
using System.Text.Json;
using TaskManager.API.Responses;

namespace TaskManager.API.Middlewares;

public class ResponseMiddleware(RequestDelegate next, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await next(context);

            if (context.Response.StatusCode is >= 400 and < 600)
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                
                var bodyText = await new StreamReader(responseBody).ReadToEndAsync();
                var message = GetMessageForStatusCode(context.Response.StatusCode, bodyText);
                var response = new ApiErrorResponse(context.Response.StatusCode, message);

                await WriteJsonResponse(context, originalBodyStream, response, context.Response.StatusCode);
            }
            else
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
        catch (Exception ex)
        {
            const int statusCode = (int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment()
                ? new ApiErrorResponse(statusCode, ex.Message, ex.StackTrace)
                : new ApiErrorResponse(statusCode, ex.Message, "Internal Server Error");

            await WriteJsonResponse(context, originalBodyStream, response, statusCode);
        }
    }

    private static async Task WriteJsonResponse( HttpContext context
                                               , Stream originalBodyStream
                                               , ApiErrorResponse response
                                               , int statusCode )
    {
        context.Response.Body = originalBodyStream;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }

    private static string GetMessageForStatusCode(int statusCode, string? originalBody)
    {
        if (!string.IsNullOrWhiteSpace(originalBody) && !IsHtmlOrProblemDetails(originalBody))
            return originalBody.Trim('"');

        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Error"
        };
    }

    private static bool IsHtmlOrProblemDetails(string body)
    {
        return body.TrimStart().StartsWith($"<") || body.Contains("\"type\":");
    }
}
