using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MiniBank.Api.Application.DTOs;
using MiniBank.Api.Domain.Entities;
using MiniBank.Api.Infrastructure.Persistence;

namespace MiniBank.Api.Application.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _dbContext;
    public AccountService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(CreateAccountRequestDto request)
    {
        var account = Account.Create(request.AccountNumber, request.OwnerId);

        _dbContext.Accounts.Add(account);

        await _dbContext.SaveChangesAsync();
        
        return account.Id;
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

    public async Task<AccountResponseDto?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Accounts
        .AsNoTracking()
        .Where(account => account.Id == id)
        .Select(account => new AccountResponseDto
        {
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status,
            CreatedAt = account.CreatedAt
        })
        .FirstOrDefaultAsync();
    }
}