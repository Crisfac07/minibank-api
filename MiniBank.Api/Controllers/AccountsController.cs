using Microsoft.AspNetCore.Mvc;
using MiniBank.Api.Application.DTOs;
using MiniBank.Api.Application.Services;

namespace MiniBank.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AccountResponseDto>>> GetAll(){
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }
}