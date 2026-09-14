using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiniBank.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var arguments = context.ActionArguments.Values;

        foreach (var argument in arguments)
        {
            var argumentType = argument?.GetType();

            if (argumentType is null)
                continue;

            var validatorType = typeof(IValidator<>)
                .MakeGenericType(argumentType);

            var validator = context.HttpContext.RequestServices
                .GetService(validatorType);

            if (validator is null)
                continue;

            var method = typeof(ValidationFilter).GetMethod(
                nameof(ValidateAsync),
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static);

            var genericMethod = method!
                .MakeGenericMethod(argumentType);

            var validationTask = (Task<ValidationResult>)genericMethod.Invoke(
                null,
                new[] { validator, argument })!;

            var validationResult = await validationTask;

            if (!validationResult.IsValid)
            {
                context.Result = new BadRequestObjectResult(
                    validationResult.Errors);

                return;
            }
        }

        await next();
    }

    private static async Task<ValidationResult> ValidateAsync<T>(
        IValidator<T> validator,
        T model)
    {
        return await validator.ValidateAsync(model);
    }

}