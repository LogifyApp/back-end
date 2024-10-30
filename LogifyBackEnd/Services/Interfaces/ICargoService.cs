using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;

namespace LogifyBackEnd.Services.Interfaces;

public interface ICargoService
{
    Task<List<Cargo>> ReturnListOfCargos(int employerId);
    Task<List<Cargo>> ReturnListOfCargosByDriver(int employerId, int driverId);
    Task<Cargo> CreateCargo(CargoCreateDto cargoDto);
    Task<Cargo> UpdateDescriptionOfCargo(int cargoId, string description);
}