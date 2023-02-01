using Newtonsoft.Json;
using UniversityOrderAPI.Middleware.Models;

namespace UniversityOrderAPI.HttpClient;

public static class JsonExtension
{
    public static async Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage httpResponseMessage)
    {
        await CustomHttpClient.CheckResponse(httpResponseMessage);
        
        var json = await httpResponseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<T>(json);
        
        return value;
    }
}