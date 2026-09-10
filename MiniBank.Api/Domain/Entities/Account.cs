using MiniBank.Api.Domain.Enums;

namespace MiniBank.Api.Domain.Entities;

public class Account
{
 public Guid Id { get; private set; }
 public string AccountNumber { get; private set; } = string.Empty;
 public Guid OwnerId { get; private set; }
 public decimal Balance { get; private set; }
 public AccountStatus Status { get; private set; }
 public DateTime CreatedAt { get; private set; }    

 public static Account Create(string accountNumber, Guid ownerId)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            AccountNumber = accountNumber,
            OwnerId = ownerId,
            Balance = 0m,
            CreatedAt = DateTime.UtcNow,
            Status = AccountStatus.Active,

        };
    }
}
