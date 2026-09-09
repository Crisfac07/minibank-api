using Microsoft.EntityFrameworkCore;
using MiniBank.Api.Application.DTOs;
using MiniBank.Api.Infrastructure.Persistence;

namespace MiniBank.Api.Application.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _dbContext;
    public AccountService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<AccountResponseDto>> GetAllAsync()
    {
        var accounts = await _dbContext.Accounts
                        .AsNoTracking()
                        .Select(account=> new AccountResponseDto
                        {
                            AccountNumber = account.AccountNumber,
                            Balance = account.Balance,
                            Status = account.Status,
                            CreatedAt = account.CreatedAt
                        }).ToListAsync();                   
        return accounts;
    }
}