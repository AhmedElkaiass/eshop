using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handlers;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> _logger) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError("An error occurred: {Message} . \r\n Time of occurance {DateTime}",
                         exception.Message,
                         DateTime.UtcNow);
        (string Details, string Title, int Status) = exception switch
        {
            InternalServerErrorException => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError),
            ValidationException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, exception.GetType().Name, StatusCodes.Status404NotFound),
            BadRequestException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
            _ => (exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError)
        };
        var problemDetails = new AppProblemDetails
        {
            Title = Title,
            Status = Status,
            Detail = Details,
            Instance = httpContext.Request.Path,

        };
        problemDetails.TraceId = httpContext.TraceIdentifier;
        if (exception is ValidationException validationException)
        {
            problemDetails.Errors = validationException.Errors
                .Select(x => new Error(x.ErrorMessage, x.PropertyName))
                .ToList();
        }
        httpContext.Response.StatusCode = Status;
        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return new ValueTask<bool>(false);
    }
    public class AppProblemDetails : ProblemDetails
    {
        public string TraceId { get; set; } = string.Empty;
        public List<Error> Errors { get; set; } = new();
    }
    public record Error(string Message, string FieldName);
}
