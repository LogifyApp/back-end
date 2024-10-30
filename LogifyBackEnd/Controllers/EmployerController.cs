using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.RequestsDTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployerController(IEmployerService employerService) : ControllerBase
{
    // a. Get list of drivers for an employer
    [HttpGet("{employerId}/drivers")]
    public async Task<ActionResult<List<Driver>>> GetListOfDrivers(int employerId)
    {
        var drivers = await employerService.GetListOfDrivers(employerId);
        return Ok(drivers);
    }

    // b. Send request to driver
    [HttpPost("{employerId}/request-driver")]
    public async Task<IActionResult> SendRequestToDriver(int employerId, [FromBody] SendRequestToDriverDto dto)
    {
        var success = await employerService.SendRequestToDriver(employerId, dto.DriverPhoneNumber);
        return success ? Ok("Request sent") : NotFound("Driver not available");
    }

    // c. Soft delete driver by ending employment
    [HttpDelete("{employerId}/delete-driver/{driverId}")]
    public async Task<IActionResult> SoftDeleteDriver(int employerId, int driverId)
    {
        var success = await employerService.SoftDeleteDriver(employerId, driverId);
        return success ? Ok("Driver employment ended") : NotFound("Driver history not found");
    }
}