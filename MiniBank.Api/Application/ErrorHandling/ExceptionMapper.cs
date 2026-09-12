using Microsoft.AspNetCore.Mvc;
using MiniBank.Api.Application.Exceptions;

namespace MiniBank.Api.Application.ErrorHandling;

public class ExceptionMapper : IExceptionMapper
{
    public ProblemDetails Map(Exception exception)
    {
        return exception switch
        {
            AccountNotFoundException =>
            new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Account not found",
                Detail = exception.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error ocurred" 
            }
        };
    }
}