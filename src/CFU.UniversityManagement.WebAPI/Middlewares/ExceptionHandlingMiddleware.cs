using CFU.Domain.Seedwork;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CFU.UniversityManagement.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
    {
        try {
            await _next(context);
        }
        catch (Exception ex) when (ex is DomainException || ex is ValidationException || ex is ArgumentException) {
            var errors = ex switch
            {
                DomainException => new() { { "Validations", new string[] { ex.Message } } },
                ArgumentException => new() { { "Validations", new string[] { ex.Message } } },
                ValidationException validation => validation.Errors.ToDictionary(er => er.PropertyName, er => new string[] { er.ErrorMessage }),
                _ => new()
            };

            logger.LogWarning(ex, "Validation Exception: {@Exception}", ex);

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Instance = context.Request.Path,
                Detail = "Please refer to the errors property for additional details.",
            };

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception ex) {
            logger.LogError(ex, "Unhandled Exception: {@Exception}", ex);
            var problemDetails = new ValidationProblemDetails
            {
                Title = "Something went wrong",
                Instance = context.Request.Path,
            };
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}

public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
