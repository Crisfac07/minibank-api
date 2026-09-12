using Microsoft.AspNetCore.Mvc;

namespace MiniBank.Api.Application.ErrorHandling;

public interface IExceptionMapper
{
    ProblemDetails Map(Exception exception);
}