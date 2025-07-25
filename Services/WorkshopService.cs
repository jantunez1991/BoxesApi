using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace BoxesApi.Services;
public class WorkshopService : IWorkshopService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<WorkshopService> _logger;
    private const string CacheKey = "workshops_cache";

    public WorkshopService(HttpClient httpClient, IMemoryCache cache, ILogger<WorkshopService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
    }

    public async Task<bool> IsValidPlaceAsync(int placeId)
    {
        var workshopIds = await GetCachedWorkshopIdsAsync();
        return workshopIds.Contains(placeId);
    }

    private async Task<List<int>> GetCachedWorkshopIdsAsync()
    {
        if (_cache.TryGetValue(CacheKey, out List<int> cachedIds))
        {
            _logger.LogInformation("Usando talleres desde cache ({Count} items)", cachedIds.Count);
            return cachedIds;
        }

        _logger.LogInformation("Llamando a la API externa de talleres...");
        var response = await _httpClient.GetAsync("https://dev.tecnomcrm.com/api/v1/places/workshops");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var ids = doc.RootElement.EnumerateArray()
            .Select(e => e.GetProperty("id").GetInt32())
            .ToList();

        _cache.Set(CacheKey, ids, TimeSpan.FromMinutes(10));
        _logger.LogInformation("Talleres cacheados exitosamente ({Count} items)", ids.Count);
        return ids;
    }
}