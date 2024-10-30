using LogifyBackEnd.Models;

namespace LogifyBackEnd.Services.Interfaces;

public interface ICacheService
{
    Task SetCurrentLocationAsync(int driverId, CurrentLocation location);
    Task<CurrentLocation?> GetCurrentLocationAsync(int driverId);
}