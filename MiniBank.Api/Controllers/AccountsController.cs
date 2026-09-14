using System;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Api.Application.DTOs;
using MiniBank.Api.Application.Services;

namespace MiniBank.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController(IAccountService accountService, 
                                IValidator<CreateAccountRequestDto> validator,
                                AccountReportService accountReportService
                                ) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;
    private readonly IValidator<CreateAccountRequestDto> _validator = validator;
    private readonly AccountReportService _accountReportService = accountReportService;
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AccountResponseDto>>> GetAll(){
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateAccountRequestDto accountRequestDto)
    {
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

    [HttpGet("report/{ownerId}")]
    public async Task <ActionResult<AccountReportDto>> GetByIdAsync(Guid ownerId)
    {
        var report = await _accountReportService.GetByOwnerIdAsync(ownerId);
        if (report == null)
            return NotFound();

        return Ok(report);

    }
}