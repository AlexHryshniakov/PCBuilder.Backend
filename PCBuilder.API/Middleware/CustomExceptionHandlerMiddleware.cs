using System.Net;
using System.Text.Json;
using FluentValidation;
using PCBuilder.Application.Common.Exceptions;

namespace PCBuilder.API.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)=>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception exception)
        {
            await HandlerExceptionAsync(context, exception);
        }
    }

    private Task HandlerExceptionAsync(HttpContext context,Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result =string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
    
                var errors = 
                    validationException.Errors.Select(
                            failure => new 
                            {
                                PropertyName = failure.PropertyName,
                                ErrorMessage = failure.ErrorMessage
                            })
                        .ToList();
        
                result = JsonSerializer.Serialize(errors);
                break;
            
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new 
                {
                    error = notFoundException.Message,
                    code = (int)HttpStatusCode.NotFound
                });
                break;
            
            case DuplicateException duplicateException:
                code = HttpStatusCode.Conflict;
                result = JsonSerializer.Serialize(new 
                {
                    error = duplicateException.Message,
                    code = (int)HttpStatusCode.Conflict
                });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if(result==string.Empty)
        {
            result= JsonSerializer.Serialize(new {error = exception.Message});
        }

        return context.Response.WriteAsync(result);
    }
}