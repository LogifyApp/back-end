using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController(IDriverService driverService) : ControllerBase
{
    // a. Accept request from employer
    [HttpPut("{driverId}/accept-request")]
    public async Task<IActionResult> AcceptRequest(int driverId, [FromBody] AcceptRequestDto dto)
    {
        if (dto.DriverId != driverId)
            return BadRequest("Driver ID mismatch");

        var success = await driverService.AcceptRequest(dto.EmployerId, driverId);
        return success ? Ok("Request accepted") : NotFound("Unable to accept request");
    }
}