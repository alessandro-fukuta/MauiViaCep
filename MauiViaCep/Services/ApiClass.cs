using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class ApiClass
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task<T> GetJsonAsync<T>(string url)
    {
        var response = await httpClient.GetAsync(url);
        //  response.EnsureSuccessStatusCode();
        string json = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            json = await response.Content.ReadAsStringAsync();
        }

        try
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (JsonException)
        {
            // Se falhar como array, tenta desserializar como objeto único
            if (JsonSerializer.Deserialize<T[]>(json) is { Length: 1 } array)
            {
                // Se falhar como array, tenta desserializar como objeto único
                return array[0];
            }
            else
            {
                throw;
            }
        }
    }
}