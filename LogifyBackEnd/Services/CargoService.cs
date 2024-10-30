using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.CargosDTOs;
using LogifyBackEnd.Exceptions;
using LogifyBackEnd.Models;
using LogifyBackEnd.Models.Enums;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class CargoService(DBContext context, ILogger<CargoService> logger) : ICargoService
{
    public async Task<List<Cargo>> ReturnListOfCargos(int employerId)
    {
        if (!await context.Users.AnyAsync(u => u.Id == employerId && u.Role == "employer"))
        {
            logger.LogWarning($"Employer with ID {employerId} not found.");
            throw new InvalidUserRoleException($"User with ID {employerId} does not have the 'employer' role.");
        }

        return await context.Cargos
            .Where(c => c.EmployerUserId == employerId)
            .OrderBy(c => c.Status)
            .Include(c => c.Car)
            .Include(c => c.DriverUser)
            .Include(c => c.EmployerUser)
            .Include(c => c.Points)
            .ToListAsync();
    }

    public async Task<List<Cargo>> ReturnListOfCargosByDriver(int employerId, int driverId)
    {
        var employerExists = await context.Users.AnyAsync(u => u.Id == employerId && u.Role == "employer");
        var driverExists = await context.Users.AnyAsync(u => u.Id == driverId && u.Role == "driver");

        if (!employerExists || !driverExists)
        {
            logger.LogWarning($"Employer ID {employerId} or Driver ID {driverId} not found.");
            throw new NotFoundException($"Employer ID {employerId} or Driver with ID {driverId} not found.");
        }

        return await context.Cargos
            .Where(c => c.EmployerUserId == employerId && c.DriverUserId == driverId)
            .OrderBy(c => c.Status)
            .Include(c => c.Car)
            .Include(c => c.DriverUser)
            .Include(c => c.EmployerUser)
            .Include(c => c.Points)
            .ToListAsync();
    }

    public async Task<Cargo> CreateCargo(CargoCreateDto cargoDto)
    {
        var employerExists = await context.Users.AnyAsync(u => u.Id == cargoDto.EmployerUserId && u.Role == "employer");
        var driverExists = await context.Users.AnyAsync(u => u.Id == cargoDto.DriverUserId && u.Role == "driver");

        if (!employerExists || !driverExists)
        {
            logger.LogWarning("Invalid Employer or Driver ID.");
            throw new NotFoundException($"Invalid Employer or Driver ID.");
        }
        
        var cargo = new Cargo
        {
            Status = CargoStatus.Created,
            CreationDate = cargoDto.CreationDate,
            Description = cargoDto.Description,
            CarId = cargoDto.CarId,
            DriverUserId = cargoDto.DriverUserId,
            EmployerUserId = cargoDto.EmployerUserId
        };

        context.Cargos.Add(cargo);
        await context.SaveChangesAsync();

        await context.Entry(cargo).Collection(c => c.Points).LoadAsync();
        return cargo;
    }

    public async Task<Cargo> UpdateDescriptionOfCargo(int cargoId, string description)
    {
        var cargo = await context.Cargos.FindAsync(cargoId);
        if (cargo == null)
        {
            logger.LogWarning($"Cargo with ID {cargoId} not found.");
            return null;
        }

        cargo.Description = description;
        await context.SaveChangesAsync();

        return cargo;
    }
}
