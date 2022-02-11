using TestJob01.API.Models;
using TestJob01.ApplicationCore.Exceptions;

namespace TestJob01.API.Middleware;
public class ExceptionMiddleware {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext) {
        try {
            await _next(httpContext);
        } catch (Exception ex) {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
        var errorDetails = exception switch {
            EntityDuplicatedException ex => new ErrorDetails(StatusCodes.Status409Conflict, ex.UIMessage),
            EntityNotFoundException ex => new ErrorDetails(StatusCodes.Status404NotFound, ex.UIMessage),
            QuantityOutOfRangeException ex => new ErrorDetails(StatusCodes.Status422UnprocessableEntity,
                ex.UIMessage),
            RemainderTooSmallException ex => new ErrorDetails(StatusCodes.Status422UnprocessableEntity,
                ex.UIMessage),
            TransferWithoutItemsException ex => new ErrorDetails(StatusCodes.Status422UnprocessableEntity,
                ex.UIMessage),

            NotImplementedException _ => new ErrorDetails(StatusCodes.Status501NotImplemented,
                "Функциональность находится в разработке"),
            Exception ex => new ErrorDetails(StatusCodes.Status500InternalServerError, ex.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorDetails.StatusCode;
        await context.Response.WriteAsync(errorDetails.ToString());
    }
}
