using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MiniBank.Api.Application.Services;
using MiniBank.Api.Application.Validators;
using MiniBank.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountRequestValidator>();

builder.Services.AddScoped<IAccountService,AccountService>();


builder.Services.AddOpenApi();

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.

app.Run();