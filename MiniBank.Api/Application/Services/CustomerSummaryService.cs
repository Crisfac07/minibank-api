using System.Net.Http.Json;
using MiniBank.Api.Application.DTOs;

namespace MiniBank.Api.Application.Services;

public class CustomerSummaryService(
    IHttpClientFactory httpClientFactory
) : ICustomerSummaryService
{
    private readonly IHttpClientFactory _httpClientFactory =
        httpClientFactory;

    public async Task<CustomerSummaryDto?> GetSummaryAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("JsonPlaceHolder");

        var userTask = client.GetAsync(
            $"users/{userId}",
            cancellationToken);

        var postTask = client.GetAsync(
            $"posts?userId={userId}",
            cancellationToken);

        await Task.WhenAll(userTask, postTask);

        var userResponse = await userTask;
        var postResponse = await postTask;

        if (!userResponse.IsSuccessStatusCode ||
            !postResponse.IsSuccessStatusCode)
        {
            return null;
        }

        var user = await userResponse.Content
            .ReadFromJsonAsync<ExternalUserDto>(
                cancellationToken);

        var posts = await postResponse.Content
            .ReadFromJsonAsync<List<ExternalPostDto>>(
                cancellationToken);

        if (user is null || posts is null)
        {
            return null;
        }

        return new CustomerSummaryDto
        {
            CustomerName = user.Name,
            Email = user.Email,
            PostCount = posts.Count
        };
    }
}