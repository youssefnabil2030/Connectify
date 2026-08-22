using System.Net;
using System.Text.Json;
using connectify.application.common.exceptions;
using connectify.application.common.models;

namespace connectify.api.middlewares;

public class exceptionhandlingmiddleware
{
    private readonly RequestDelegate _next;

    public exceptionhandlingmiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = exception.Message;

        switch (exception)
        {
            case validationexception:
                code = HttpStatusCode.BadRequest;
                break;
            case notfoundexception:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var response = apiresponse<string>.failure(result);
        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });

        return context.Response.WriteAsync(jsonResponse);
    }
}
