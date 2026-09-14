using MiniBank.Api.Application.DTOs;

namespace MiniBank.Api.Application.Services;

public interface ICustomerSummaryService
{
    Task<CustomerSummaryDto?> GetSummaryAsync(
        int userId,
        CancellationToken cancellationToken);
}