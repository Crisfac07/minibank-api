using Microsoft.EntityFrameworkCore;
using MiniBank.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();