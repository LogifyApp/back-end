using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController(ICarService carService) : ControllerBase
{
    [HttpPost("AddCar")]
    public async Task<IActionResult> AddCar(int employerId, string number, string? model = null, string? brand = null)
    {
        var car = await carService.AddCar(employerId, number, model, brand);
        return Ok(car);
    }

    [HttpPut("UpdateCar")]
    public async Task<IActionResult> UpdateCar(string carNumber, string? model, string? brand, bool status)
    {
        var car = await carService.UpdateCar(carNumber, model, brand, status);
        if (car == null)
        {
            return NotFound();
        }
        return Ok(car);
    }

    [HttpDelete("SoftDeleteCar")]
    public async Task<IActionResult> SoftDeleteCar(string carNumber)
    {
        var car = await carService.SoftDeleteCar(carNumber);
        if (car == null)
        {
            return NotFound();
        }
        return Ok(car);
    }

    [HttpGet("GetCar")]
    public async Task<IActionResult> GetCar(string carNumber)
    {
        var car = await carService.GetCar(carNumber);
        if (car == null)
        {
            return NotFound();
        }

        return Ok(car);
    }
    [HttpGet("GetListOfCars")]
    public async Task<IActionResult> GetListOfCars(int employerId)
    {
        var cars = await carService.GetListOfCars(employerId);
        if (cars == null || cars.Count == 0)
        {
            return NotFound("No cars found for the specified employer.");
        }
        return Ok(cars);
    }
}
