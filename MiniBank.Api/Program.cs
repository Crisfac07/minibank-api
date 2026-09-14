using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

var jwtSection = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSection["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured");
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidIssuer = jwtSection["Issuer"],

          ValidateAudience = true,
          ValidAudience = jwtSection["Audience"],

          ValidateLifetime = true,

          ValidateIssuerSigningKey = true,
          IssuerSigningKey = 
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))  
        };
    })
        ;


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();