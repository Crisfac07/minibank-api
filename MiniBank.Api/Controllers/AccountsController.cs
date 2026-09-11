using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Api.Application.DTOs;
using MiniBank.Api.Application.Services;

namespace MiniBank.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController(IAccountService accountService, 
                                IValidator<CreateAccountRequestDto> validator
                                ) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;
    private readonly IValidator<CreateAccountRequestDto> _validator = validator;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AccountResponseDto>>> GetAll(){
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateAccountRequestDto accountRequestDto)
    {
        var validationResult = await _validator.ValidateAsync(accountRequestDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

       var id = await _accountService.CreateAsync(accountRequestDto);
       return CreatedAtAction(nameof(GetById), new{id}, id);      
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountResponseDto>> GetById(Guid id)
    {
        var account = await _accountService.GetByIdAsync(id);
        if(account == null)
            return NotFound();
        return Ok(account);
    }
}