using MauiViaCep.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class ApiClass
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task<List<ViaCep>> GetJsonAsync(string url)
    {
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(json))
        {
            throw new Exception("Resposta da API vazia.");
        }

        if (json.Contains("\"erro\":true"))
        {
            throw new Exception("Endereço não encontrado na API ViaCEP.");
        }

        try
        {
            // Primeiro, tenta desserializar como um objeto único
            var singleResult = JsonSerializer.Deserialize<ViaCep>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (singleResult != null)
            {
                return new List<ViaCep> { singleResult }; // Retorna como lista
            }
        }
        catch (JsonException)
        {
            try
            {
                // Se falhar, tenta desserializar como array de objetos
                var multipleResults = JsonSerializer.Deserialize<List<ViaCep>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (multipleResults?.Count > 0)
                {
                    return multipleResults; // Retorna a lista completa
                }
            }
            catch (JsonException)
            {
                throw new Exception("Falha na conversão do JSON para ViaCep.");
            }
        }

        throw new Exception("Erro inesperado na desserialização.");
    }


}