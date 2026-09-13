namespace  MiniBank.Api.Application.Repositories;

public interface IAccountReportRepository
{
    public Task<AccountReportDto?> GetByOwnerIdAsync(Guid ownerId);
}