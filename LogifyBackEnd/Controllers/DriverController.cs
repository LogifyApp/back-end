using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriverController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    // a. Accept request from employer
    [HttpPost("{driverId}/accept-request")]
    public async Task<IActionResult> AcceptRequest(int driverId, [FromBody] AcceptRequestDto dto)
    {
        if (dto.DriverId != driverId)
            return BadRequest("Driver ID mismatch");

        var success = await _driverService.AcceptRequest(dto.EmployerId, driverId);
        return success ? Ok("Request accepted") : NotFound("Unable to accept request");
    }
}