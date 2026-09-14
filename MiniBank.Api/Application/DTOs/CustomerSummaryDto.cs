namespace MiniBank.Api.Application.DTOs;

public class CustomerSummaryDto
{
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int PostCount { get; set; }
}