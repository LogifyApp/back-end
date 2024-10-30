using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    private readonly ICacheService _cacheService;

    public LocationController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    // Endpoint for Driver to update location
    [HttpPost("update")]
    public async Task<IActionResult> UpdateLocation([FromBody] CurrentLocation location)
    {
        await _cacheService.SetCurrentLocationAsync(location.DriverId, location);
        return Ok("Location updated successfully");
    }

    // Endpoint for Employer to retrieve Driver's current location
    [HttpGet("retrieve/{driverId}")]
    public async Task<IActionResult> GetCurrentLocation(int driverId)
    {
        var location = await _cacheService.GetCurrentLocationAsync(driverId);
        if (location == null) return NotFound("Location not found");
        return Ok(location);
    }
}