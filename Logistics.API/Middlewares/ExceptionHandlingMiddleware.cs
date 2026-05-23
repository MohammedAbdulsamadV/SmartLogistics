using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Logistics.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
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
        context.Response.ContentType = "application/json";
    
        if (exception is Logistics.Application.Common.Exceptions.ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        
            var validationResponse = new
            {
                title = "Validation Error",
                status = StatusCodes.Status400BadRequest,
                detail = "One or more validation errors occurred",
                errors = validationException.Errors 
            };
        
            return context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    
        var genericResponse = new 
        { 
            title = "Internal Server Error", 
            status = StatusCodes.Status500InternalServerError, 
            detail = exception.Message 
        };
    
        return context.Response.WriteAsync(JsonSerializer.Serialize(genericResponse));
    }
}