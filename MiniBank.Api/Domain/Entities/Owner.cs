namespace MiniBank.Api.Domain.Entities;

public class Owner{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new();
}
