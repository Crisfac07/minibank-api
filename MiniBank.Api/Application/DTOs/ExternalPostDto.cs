namespace MiniBank.Api.Application.DTOs;

public class ExternalPostDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
}