using LogifyBackEnd.Data.DTOs.PointsDTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PointController : ControllerBase
{
    private readonly IPointService _pointService;

    public PointController(IPointService pointService)
    {
        _pointService = pointService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePoint([FromBody] CreatePointDto dto)
    {
        var point = await _pointService.CreatePoint(dto);
        return Ok(point);
    }
    
    [HttpPost("create-list")]
    public async Task<IActionResult> CreatePoints([FromBody] CreateListOfPointsDto dto)
    {
        var points = await _pointService.CreateListOfPoints(dto.Points);
        return Ok(points);
    }

    [HttpGet("{pointId}")]
    public async Task<IActionResult> GetPoint(int pointId)
    {
        var point = await _pointService.GetPoint(pointId);
        return point != null ? Ok(point) : NotFound("Point not found");
    }

    [HttpGet("cargo/{cargoId}")]
    public async Task<IActionResult> GetPointsByCargoId(int cargoId)
    {
        var points = await _pointService.GetPointsByCargoId(cargoId);
        return Ok(points);
    }

    [HttpPut("{pointId}/update")]
    public async Task<IActionResult> UpdatePoint(int pointId, [FromBody] UpdatePointDto dto)
    {
        var success = await _pointService.UpdatePoint(pointId, dto);
        return success ? Ok("Point updated") : NotFound("Point not found");
    }

    [HttpDelete("{pointId}")]
    public async Task<IActionResult> DeletePoint(int pointId)
    {
        var success = await _pointService.DeletePoint(pointId);
        return success ? Ok("Point deleted") : NotFound("Point not found");
    }
}