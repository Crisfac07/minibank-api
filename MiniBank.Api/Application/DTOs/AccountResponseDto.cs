using MiniBank.Api.Domain.Enums;

namespace MiniBank.Api.Application.DTOs;

public class AccountResponseDto
{
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public AccountStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}