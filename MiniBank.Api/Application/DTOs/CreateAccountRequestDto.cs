using MiniBank.Api.Domain.Enums;

namespace MiniBank.Api.Application.DTOs;

public class CreateAccountRequestDto
{
 public string AccountNumber { get; set; } = string.Empty;
 public Guid OwnerId { get; set; }
}