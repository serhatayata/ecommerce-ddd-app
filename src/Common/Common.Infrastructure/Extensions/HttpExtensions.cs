using System.Net.Http.Json;

namespace Common.Infrastructure.Extensions;

public static class HttpExtensions
{
    public async static Task<TResult?> PostAsync<TResult, TValue>(
    this HttpClient client,
    string url,
    TValue value,
    CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync(url, value, cancellationToken);

        return response.IsSuccessStatusCode ?
            await response.Content.ReadFromJsonAsync<TResult?>() : 
            default;
    }
}