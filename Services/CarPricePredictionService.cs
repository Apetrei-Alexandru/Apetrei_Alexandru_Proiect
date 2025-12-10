using Apetrei_Alexandru_Proiect.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Apetrei_Alexandru_Proiect.Services
{
    public class CarPricePredictionService : ICarPricePredictionService
    {
        private readonly HttpClient _httpClient;

        public CarPricePredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<float> PredictPriceAsync(CarPriceInput input)
        {
            // POST către /predict în Web API-ul ML.NET
            var response = await _httpClient.PostAsJsonAsync("/predict", input).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // Deserialize răspuns JSON { "score": 48810.246 }
            var result = await response.Content.ReadFromJsonAsync<CarPriceApiResponse>().ConfigureAwait(false)
                         ?? throw new HttpRequestException("API returned null response");

            return result.Score;
        }

        // Maparea JSON
        private class CarPriceApiResponse
        {
            public float Score { get; set; }
        }
    }
}
