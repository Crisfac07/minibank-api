using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MiniBank.Api.Application.ErrorHandling;
using MiniBank.Api.Application.Repositories;
using MiniBank.Api.Application.Services;
using MiniBank.Api.Application.Validators;
using MiniBank.Api.Filters;
using MiniBank.Api.Infrastructure.Persistence;
using MiniBank.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddScoped<IExceptionMapper, ExceptionMapper>();
builder.Services.AddScoped<IAccountReportRepository, DapperAccountReportRepository>();
builder.Services.AddScoped<AccountReportService>();

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
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();