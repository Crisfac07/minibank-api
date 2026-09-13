using MiniBank.Api.Application.DTOs;
public class AccountReportDto
{
    public string OwnerName {get;set;} = string.Empty;  
    public int TotalAccounts {get; set;}
    public decimal TotalBalance {get; set;}
}