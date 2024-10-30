using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs.PointsDTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class PointService(DBContext context) : IPointService
{
    public async Task<List<PointDto>> CreateListOfPoints(List<CreatePointDto> createPointDtos)
    {
        var points = createPointDtos.Select(dto => new Point
            {
                Label = dto.Label,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Order = dto.Order,
                CargoId = dto.CargoId
            })
            .ToList();

        context.Points.AddRange(points);
        await context.SaveChangesAsync();

        return points.Select(p => new PointDto
        {
            Id = p.Id,
            Label = p.Label,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
            Order = p.Order,
            CargoId = p.CargoId
        }).ToList();
    }
    public async Task<PointDto> CreatePoint(CreatePointDto createPointDto)
    {
        var point = new Point
        {
            Label = createPointDto.Label,
            Latitude = createPointDto.Latitude,
            Longitude = createPointDto.Longitude,
            Order = createPointDto.Order,
            CargoId = createPointDto.CargoId
        };

        context.Points.Add(point);
        await context.SaveChangesAsync();

        return new PointDto
        {
            Id = point.Id,
            Label = point.Label,
            Latitude = point.Latitude,
            Longitude = point.Longitude,
            Order = point.Order,
            CargoId = point.CargoId
        };
    }

    public async Task<PointDto?> GetPoint(int pointId)
    {
        var point = await context.Points.FindAsync(pointId);
        if (point == null) return null;

        return new PointDto
        {
            Id = point.Id,
            Label = point.Label,
            Latitude = point.Latitude,
            Longitude = point.Longitude,
            Order = point.Order,
            CargoId = point.CargoId
        };
    }

    public async Task<List<PointDto>> GetPointsByCargoId(int cargoId)
    {
        return await context.Points
            .Where(p => p.CargoId == cargoId)
            .Select(p => new PointDto
            {
                Id = p.Id,
                Label = p.Label,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                Order = p.Order,
                CargoId = p.CargoId
            })
            .ToListAsync();
    }

    public async Task<bool> UpdatePoint(int pointId, UpdatePointDto updatePointDto)
    {
        var point = await context.Points.FindAsync(pointId);
        if (point == null) return false;

        point.Label = updatePointDto.Label;
        point.Latitude = updatePointDto.Latitude;
        point.Longitude = updatePointDto.Longitude;
        point.Order = updatePointDto.Order;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePoint(int pointId)
    {
        var point = await context.Points.FindAsync(pointId);
        if (point == null) return false;

        context.Points.Remove(point);
        await context.SaveChangesAsync();
        return true;
    }
}