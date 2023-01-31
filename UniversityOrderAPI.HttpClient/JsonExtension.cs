using Newtonsoft.Json;

namespace UniversityOrderAPI.HttpClient;

public static class JsonExtension
{
    public static async Task<T> ReasAsJsonAsync<T>(this HttpContent content)
    {
        string json = await content.ReadAsStringAsync();
        T value = JsonConvert.DeserializeObject<T>(json);
        return value;
    }
}