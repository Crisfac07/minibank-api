using Microsoft.EntityFrameworkCore;
using MiniBank.Api.Domain.Entities;

namespace MiniBank.Api.Infrastructure.Persistence;

public class AppDbContext (DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts{ get; set; }
    public DbSet<Owner> Owners{ get; set; }
}