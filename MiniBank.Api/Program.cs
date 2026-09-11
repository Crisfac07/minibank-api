using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MiniBank.Api.Application.Services;
using MiniBank.Api.Application.Validators;
using MiniBank.Api.Filters;
using MiniBank.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ValidationFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ValidationFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection"));
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountRequestValidator>();

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();

app.Run();