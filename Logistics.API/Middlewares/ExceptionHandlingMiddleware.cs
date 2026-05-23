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
    
        // 1. استخدام Pattern Matching للتعرف على نوع الـ Exception وعمل Cast له في نفس السطر
        if (exception is Logistics.Application.Common.Exceptions.ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        
            var validationResponse = new
            {
                title = "Validation Error",
                status = StatusCodes.Status400BadRequest,
                detail = "واحد أو أكثر من البيانات المدخلة غير سليمة.",
                // الآن سيقرأ الخاصية بنجاح لأن الكومبيلر علم أن نوعه الـ Custom Exception بتاعنا
                errors = validationException.Errors 
            };
        
            return context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
        }

        // 2. التعامل مع أي خطأ سيرفر عام غير متوقع (500)
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