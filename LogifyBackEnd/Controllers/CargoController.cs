using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CargoController : ControllerBase
{
    private readonly ICargoService _cargoService;

    public CargoController(ICargoService cargoService)
    {
        _cargoService = cargoService;
    }

    [HttpGet("by-employer/{employerId}")]
    public async Task<ActionResult<List<Cargo>>> GetCargosByEmployer(int employerId)
    {
        var cargos = await _cargoService.ReturnListOfCargos(employerId);
        return Ok(cargos);
    }

    [HttpGet("by-employer-and-driver/{employerId}/{driverId}")]
    public async Task<ActionResult<List<Cargo>>> GetCargosByEmployerAndDriver(int employerId, int driverId)
    {
        var cargos = await _cargoService.ReturnListOfCargosByDriver(employerId, driverId);
        return Ok(cargos);
    }

    [HttpPost]
    public async Task<ActionResult<Cargo>> CreateCargo([FromBody] CargoCreateDto cargoDto)
    {
        var createdCargo = await _cargoService.CreateCargo(cargoDto);
        return CreatedAtAction(nameof(CreateCargo), new { id = createdCargo.Id }, createdCargo);
    }

    [HttpPut("{cargoId}/description")]
    public async Task<ActionResult<Cargo>> UpdateCargoDescription(int cargoId, [FromBody] string description)
    {
        var updatedCargo = await _cargoService.UpdateDescriptionOfCargo(cargoId, description);
        if (updatedCargo == null) return NotFound();

        return Ok(updatedCargo);
    }
}