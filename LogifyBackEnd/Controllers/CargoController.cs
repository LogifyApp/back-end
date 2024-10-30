using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.CargosDTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CargoController(ICargoService cargoService) : ControllerBase
{
    [HttpGet("by-employer/{employerId}")]
    public async Task<ActionResult<List<Cargo>>> GetCargosByEmployer(int employerId)
    {
        var cargos = await cargoService.ReturnListOfCargos(employerId);
        return Ok(cargos);
    }

    [HttpGet("by-employer-and-driver/{employerId}/{driverId}")]
    public async Task<ActionResult<List<Cargo>>> GetCargosByEmployerAndDriver(int employerId, int driverId)
    {
        var cargos = await cargoService.ReturnListOfCargosByDriver(employerId, driverId);
        return Ok(cargos);
    }

    [HttpPost]
    public async Task<ActionResult<Cargo>> CreateCargo([FromBody] CargoCreateDto cargoDto)
    {
        var createdCargo = await cargoService.CreateCargo(cargoDto);
        return CreatedAtAction(nameof(CreateCargo), new { id = createdCargo.Id }, createdCargo);
    }

    [HttpPut("{cargoId}/description")]
    public async Task<ActionResult<Cargo>> UpdateCargoDescription(int cargoId, [FromBody] string description)
    {
        var updatedCargo = await cargoService.UpdateDescriptionOfCargo(cargoId, description);
        if (updatedCargo == null) return NotFound();

        return Ok(updatedCargo);
    }
}