using Newtonsoft.Json;

namespace UniversityOrderAPI.HttpClient;

public static class HttpContentExtensions
{
    public static async Task<T> ReasAsJsonAsync<T>(this HttpContent content)
    {
        string json = await content.ReadAsStringAsync();
        T value = JsonConvert.DeserializeObject<T>(json) ?? throw new InvalidOperationException("Json string is null");
        return value;
    }
}