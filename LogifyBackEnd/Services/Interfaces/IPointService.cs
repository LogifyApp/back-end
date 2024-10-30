using LogifyBackEnd.Data.DTOs.PointsDTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IPointService
{
    Task<List<PointDto>> CreateListOfPoints(List<CreatePointDto> createPointDtos);
    Task<PointDto> CreatePoint(CreatePointDto createPointDto);
    Task<PointDto?> GetPoint(int pointId);
    Task<List<PointDto>> GetPointsByCargoId(int cargoId);
    Task<bool> UpdatePoint(int pointId, UpdatePointDto updatePointDto);
    Task<bool> DeletePoint(int pointId);
}