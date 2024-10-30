using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using StackExchange.Redis;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace LogifyBackEnd.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _database;

    public CacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task SetCurrentLocationAsync(int driverId, CurrentLocation location)
    {
        var locationJson = JsonConvert.SerializeObject(location);
        await _database.StringSetAsync($"currentlocation:{driverId}", locationJson);
    }

    public async Task<CurrentLocation?> GetCurrentLocationAsync(int driverId)
    {
        var locationJson = await _database.StringGetAsync($"currentlocation:{driverId}");
        return locationJson.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<CurrentLocation>(locationJson);
    }
}