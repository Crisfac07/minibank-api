using FluentValidation;
using MiniBank.Api.Application.DTOs;

namespace MiniBank.Api.Application.Validators;

public class CreateAccountRequestValidator 
: AbstractValidator<CreateAccountRequestDto>
{
 public CreateAccountRequestValidator()
    {
        RuleFor(x=> x.AccountNumber).NotEmpty().WithMessage("Account number is required");
        RuleFor(x => x.AccountNumber).MaximumLength(20).WithMessage("The maximum length is 20 characters");

        RuleFor(x=> x.OwnerId).NotEmpty().WithMessage("The OwnerId is required");
    }
}