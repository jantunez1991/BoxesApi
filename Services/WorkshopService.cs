using System.Text.Json;

namespace BoxesApi.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly HttpClient _httpClient;
        private List<int>? _cachedWorkshopIds;

        public WorkshopService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsValidPlaceAsync(int placeId)
        {
            if (_cachedWorkshopIds == null)
            {
                var response = await _httpClient.GetAsync("https://dev.tecnomcrm.com/api/v1/places/workshops");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                _cachedWorkshopIds = doc.RootElement.EnumerateArray()
                    .Select(e => e.GetProperty("id").GetInt32()).ToList();
            }

            return _cachedWorkshopIds.Contains(placeId);
        }
    }
}
