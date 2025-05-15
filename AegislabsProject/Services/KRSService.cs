using AegislabsProject.Models.ModelDTO;
using Newtonsoft.Json;

namespace AegislabsProject.Services
{
    public class KRSService
    {
        private readonly HttpClient _client;

        public KRSService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7291/");
        }

        public async Task<List<KRSDto>> GetAllAsync()
        {
            var response = await _client.GetAsync("api/KRS");
            if (!response.IsSuccessStatusCode) return new List<KRSDto>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<KRSDto>>(json);
        }
    }
}
