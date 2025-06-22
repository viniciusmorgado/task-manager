using System.Text.Json.Serialization;

namespace TaskManager.API.Responses;

public class ApiErrorResponse(int statusCode, string message, string? stackTrace = null)
{
    public string StatusCode { get; set; } = statusCode.ToString();
    public string Message { get; set; } = message;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StackTrace { get; set; } = stackTrace;
}
