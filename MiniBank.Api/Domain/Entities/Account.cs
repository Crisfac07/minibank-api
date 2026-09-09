using MiniBank.Api.Domain.Enums;

namespace MiniBank.Api.Domain.Entities;

public class Account
{
 public Guid Id { get; set; }
 public string AccountNumber { get; set; } = string.Empty;
 public Guid OwnerId { get; set; }
 public decimal Balance { get; set; }
 public AccountStatus Status { get; set; }
 public DateTime CreatedAt { get; set; }    
}
