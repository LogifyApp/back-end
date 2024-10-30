using LogifyBackEnd.Data.DTOs.PointsDTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PointController(IPointService pointService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreatePoint([FromBody] CreatePointDto dto)
    {
        var point = await pointService.CreatePoint(dto);
        return Ok(point);
    }
    
    [HttpPost("create-list")]
    public async Task<IActionResult> CreatePoints([FromBody] CreateListOfPointsDto dto)
    {
        var points = await pointService.CreateListOfPoints(dto.Points);
        return Ok(points);
    }

    [HttpGet("{pointId}")]
    public async Task<IActionResult> GetPoint(int pointId)
    {
        var point = await pointService.GetPoint(pointId);
        return point != null ? Ok(point) : NotFound("Point not found");
    }

    [HttpGet("cargo/{cargoId}")]
    public async Task<IActionResult> GetPointsByCargoId(int cargoId)
    {
        var points = await pointService.GetPointsByCargoId(cargoId);
        return Ok(points);
    }

    [HttpPut("{pointId}/update")]
    public async Task<IActionResult> UpdatePoint(int pointId, [FromBody] UpdatePointDto dto)
    {
        var success = await pointService.UpdatePoint(pointId, dto);
        return success ? Ok("Point updated") : NotFound("Point not found");
    }

    [HttpDelete("{pointId}")]
    public async Task<IActionResult> DeletePoint(int pointId)
    {
        var success = await pointService.DeletePoint(pointId);
        return success ? Ok("Point deleted") : NotFound("Point not found");
    }
}