using MiniBank.Api.Application.DTOs;

namespace MiniBank.Api.Application.Services;

public interface IAccountService
{
    Task<IReadOnlyList<AccountResponseDto>> GetAllAsync();
    Task<Guid>CreateAsync(CreateAccountRequestDto request);
    Task<AccountResponseDto?> GetByIdAsync(Guid id);
}