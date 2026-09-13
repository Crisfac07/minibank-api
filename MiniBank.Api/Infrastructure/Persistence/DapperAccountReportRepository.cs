using Dapper;
using Microsoft.Data.SqlClient;
using MiniBank.Api.Application.Repositories;

namespace MiniBank.Api.Infrastructure.Persistence;

public class DapperAccountReportRepository(IConfiguration configuration) : IAccountReportRepository
{
    private readonly string _connectionString = 
    configuration.GetConnectionString("SqlServerConnection") ?? 
    throw new InvalidOperationException("SqlServerConnection is not configured ");
    public async Task<AccountReportDto?> GetByOwnerIdAsync(Guid ownerId)
    {
        const string sql = """
            SELECT
                CONCAT(o.Name, ' ', o.LastName) AS OwnerName,
                COUNT(a.Id) AS TotalAccounts,
                COALESCE(SUM(a.Balance), 0) AS TotalBalance
            FROM Owners o
            INNER JOIN Accounts a
                ON o.Id = a.OwnerId
            WHERE o.Id = @OwnerId
            GROUP BY o.Id, o.Name, o.LastName;
            """;

        await using var connection = new SqlConnection(_connectionString);

        return await connection.QuerySingleOrDefaultAsync<AccountReportDto>(
            sql,
            new { OwnerId = ownerId}
        );
    }
}