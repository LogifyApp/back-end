using LogifyBackEnd.Data;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class CargoService : ICargoService
{
    private readonly DBContext _context;

    public CargoService(DBContext context)
    {
        _context = context;
    }

    public async Task<List<Cargo>> ReturnListOfCargos(int employerId)
    {
        return await _context.Cargos
            .Where(c => c.EmployerUserId == employerId)
            .OrderBy(c => c.Status) // Sorting by status
            .Include(c => c.Car)
            .Include(c => c.Driver)
            .Include(c => c.Employer)
            .Include(c => c.Points) // Include Points
            .ToListAsync();
    }

    public async Task<List<Cargo>> ReturnListOfCargosByDriver(int employerId, int driverId)
    {
        return await _context.Cargos
            .Where(c => c.EmployerUserId == employerId && c.DriverUserId == driverId)
            .OrderBy(c => c.Status)
            .Include(c => c.Car)
            .Include(c => c.Driver)
            .Include(c => c.Employer)
            .Include(c => c.Points) // Include Points
            .ToListAsync();
    }


    public async Task<Cargo> CreateCargo(Cargo cargo)
    {
        _context.Cargos.Add(cargo);
        await _context.SaveChangesAsync();
        
        // Load related points for the cargo (assuming points are added afterward)
        await _context.Entry(cargo).Collection(c => c.Points).LoadAsync();

        return cargo;
    }

    public async Task<Cargo> UpdateDescriptionOfCargo(int cargoId, string description)
    {
        var cargo = await _context.Cargos.FindAsync(cargoId);
        if (cargo == null) return null;

        cargo.Description = description;
        await _context.SaveChangesAsync();

        return cargo;
    }
}