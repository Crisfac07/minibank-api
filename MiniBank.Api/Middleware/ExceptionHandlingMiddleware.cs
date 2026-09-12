using MiniBank.Api.Application.ErrorHandling;

namespace MiniBank.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next,
                                        ILogger<ExceptionHandlingMiddleware> logger
                                        )
{
    private readonly RequestDelegate _next = next;  
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    public async Task InvokeAsync(
        HttpContext context,
        IExceptionMapper exceptionMapper
    )
    {
        try
        {
            await _next(context);
        }catch(Exception exception)
        {
            _logger.LogError(exception, 
            "An unhandled exception occurred."
            );
            if (context.Response.HasStarted)
            {
                _logger.LogWarning(
                "The response has already started, so the exception cannot be handled by the global exception handler.");
                
                throw;
            }

            var problemDetails = exceptionMapper.Map(exception);
            context.Response.StatusCode = problemDetails.Status 
                                        ?? StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problemDetails);
        }          
    }
}