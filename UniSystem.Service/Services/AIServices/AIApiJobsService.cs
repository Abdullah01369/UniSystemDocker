using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace UniSystem.Service.Services.AIServices
{
    public class AIApiJobsService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public AIApiJobsService(IConfiguration configuration, HttpClient httpClient)
        {
            _apiKey = configuration["OpenAI:ApiKey"];
            _httpClient = httpClient;
        }

        public async Task<string> GenerateSQLQuery(string databaseInfo, string userQuestion)
        {
            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "Sen bir SQL uzmanısın. Veritabanı şeması ve ilişkiler hakkında bilgi verilecek ve buna uygun SQL sorgusu üretmelisin." },
                new { role = "user", content = "Veritabanı yapısı ve ilişkileri: " + databaseInfo },
                new { role = "user", content = "Soru: " + userQuestion }
            }
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }

}






