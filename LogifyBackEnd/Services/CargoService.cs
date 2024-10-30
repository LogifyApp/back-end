using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Models.Enums;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class CargoService : ICargoService
{
    private readonly DBContext _context;
    private readonly ILogger<CargoService> _logger;

    public CargoService(DBContext context, ILogger<CargoService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Cargo>> ReturnListOfCargos(int employerId)
    {
        // Check if Employer exists
        if (!await _context.Users.AnyAsync(u => u.Id == employerId && u.Role == "employer"))
        {
            _logger.LogWarning($"Employer with ID {employerId} not found.");
            return null; // Or throw a custom NotFoundException
        }

        return await _context.Cargos
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
        // Check if Employer and Driver exist
        var employerExists = await _context.Users.AnyAsync(u => u.Id == employerId && u.Role == "employer");
        var driverExists = await _context.Users.AnyAsync(u => u.Id == driverId && u.Role == "driver");

        if (!employerExists || !driverExists)
        {
            _logger.LogWarning($"Employer ID {employerId} or Driver ID {driverId} not found.");
            return null; // Or throw a custom NotFoundException
        }

        return await _context.Cargos
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
        // Ensure cargo has a valid EmployerUserId and DriverUserId
        var employerExists = await _context.Users.AnyAsync(u => u.Id == cargoDto.EmployerUserId && u.Role == "employer");
        var driverExists = await _context.Users.AnyAsync(u => u.Id == cargoDto.DriverUserId && u.Role == "driver");

        if (!employerExists || !driverExists)
        {
            _logger.LogWarning("Invalid Employer or Driver ID.");
            return null; // Or throw an exception
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

        _context.Cargos.Add(cargo);
        await _context.SaveChangesAsync();

        await _context.Entry(cargo).Collection(c => c.Points).LoadAsync();
        return cargo;
    }

    public async Task<Cargo> UpdateDescriptionOfCargo(int cargoId, string description)
    {
        var cargo = await _context.Cargos.FindAsync(cargoId);
        if (cargo == null)
        {
            _logger.LogWarning($"Cargo with ID {cargoId} not found.");
            return null;
        }

        cargo.Description = description;
        await _context.SaveChangesAsync();

        return cargo;
    }
}
