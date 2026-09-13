using MiniBank.Api.Application.Repositories;

namespace MiniBank.Api.Application.Services;

public class AccountReportService(IAccountReportRepository accountReportRepository )
{
    private readonly IAccountReportRepository _accountReportRepository = accountReportRepository;
     public async Task<AccountReportDto?> GetByOwnerIdAsync(Guid ownerId)
    {
        return await _accountReportRepository.GetByOwnerIdAsync(ownerId);
    }
    
}