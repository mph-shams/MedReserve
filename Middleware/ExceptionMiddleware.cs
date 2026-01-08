using FluentValidation;
using MedReserve.Application.Common.Models;
using System.Net;
using System.Text.Json;

namespace MedReserve.WebAPI.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch (Exception ex) { await HandleExceptionAsync(context, ex); }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = new ErrorResponse { StatusCode = (int)HttpStatusCode.InternalServerError };

        if (exception is ValidationException validationEx)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.StatusCode = 400;
            response.Message = "Input validation error!";
            response.Details = string.Join(" | ", validationEx.Errors.Select(e => e.ErrorMessage));
        }
        else
        {
            logger.LogError(exception, exception.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "Internal Server Error";
        }
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}