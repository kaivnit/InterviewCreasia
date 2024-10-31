using Interview.Application.Enums;
using System.Text.Json;

namespace Interview.Application.Models;

public class ResponseModel<T>
{
    public ResponseModel()
    {

    }
    public ResponseModel(StatusCodeEnum statusCode, string message, T? data)
    {
        Message = message;
        StatusCode = statusCode;
        ResponseData = data;
    }
    public StatusCodeEnum StatusCode { get; set; } = StatusCodeEnum.Unknown;
    public string Message { get; set; } = string.Empty;
    public T? ResponseData { get; set; }

    public override string ToString()
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Serialize(this, options);
    }
}
